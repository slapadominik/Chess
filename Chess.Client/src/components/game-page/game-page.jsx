import React from 'react';

import * as S from './game-page.styled';
import { Chessboard } from './chessboard/chessboard';
import { PlayerPanel } from './player-panel/player-panel';

export class GamePage extends React.Component {
  state = {
    isFinished: false,
    winner: null,
  };

  render() {
    return (
      <S.GamePage>
        <div className="game-page">
          <Chessboard
            hubConnection={this.props.hubConnection}
            gameId={this.props.gameId}
            currentPerson={this.props.currentPerson}
            players={this.props.players}
            isFinished={this.state.isFinished}
            hasStarted={this.props.hasStarted}
          />
          <PlayerPanel
            startTheGame={this.props.startTheGame}
            createTheGame={this.props.createTheGame}
            players={this.props.players}
            onPlayerJoin={this.props.onPlayerJoin}
            onResetClick={this.props.onResetClick}
            tableNumber={this.props.tableNumber}
            isFinished={this.state.isFinished}
            winner={this.state.winner}
            hasCreated={this.props.hasCreated}
            hasStarted={this.props.hasStarted}
          />
        </div>
      </S.GamePage>
    );
  }
}
