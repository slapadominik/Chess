import { isPersonAlreadyPlayer } from '../../app.utils';

const React = require('react');
const Chess = require('react-chess');

export class Chessboard extends React.PureComponent {
  state = { pieces: Chess.getDefaultLineup() };

  componentDidMount() {
    this.props.hubConnection.on('MakeMove_Result', (moveResult) => {
      this.setState({
        pieces: [
          ...this.state.pieces,
          `${moveResult.chess}@${moveResult.to}`,
        ].filter(
          (x) =>
            x !== `${moveResult.chess}@${moveResult.from}` &&
            x !==
              `${moveResult.captured ? moveResult.captured.charAt(0) : ''}@${
                moveResult.to
              }`,
        ),
      });
    });

    this.props.hubConnection.on('MakeMove_Error', (errorMsg, from) =>{
      console.log(errorMsg);
      let chess = this.state.pieces.filter(x => x.includes(from))[0];
      this.setState({pieces: this.state.pieces.filter(x => x !== chess)});
      this.setState({pieces: [...this.state.pieces, chess]});
    });
  }

  makeMove = (from, to) => {
      this.props.hubConnection.invoke(
        'MakeMove',
        this.props.gameId,
        this.props.currentPerson.id,
        from,
        to,
      );
  }

  handleMovePiece = (piece, fromSquare = piece.position, toSquare) => {
    if (!isPersonAlreadyPlayer(this.props.players, this.props.currentPerson))
      return;

    this.makeMove(fromSquare, toSquare);
    

    // const newPieces = this.state.pieces
    //   .map((curr, index) => {
    //     // if (piece.index === index) {
    //     //   return `${piece.name}@${toSquare}`;
    //     // } else if (curr.indexOf(toSquare) === 2) {
    //     //   return false; // To be removed from the board
    //     //   // obecnie mozna bic swoich
    //     //   // obecnie mozna poruszac sie po calej mapie
    //     // }
    //     return curr;
    //   })
    //   .filter(Boolean);

    // this.setState({ pieces: newPieces });
  };

  render() {
    return (
      <div className="chessboard">
        <Chess
          allowMoves={this.props.hasStarted && !this.props.isFinished}
          pieces={this.state.pieces}
          onMovePiece={this.handleMovePiece}
        />
      </div>
    );
  }
}
