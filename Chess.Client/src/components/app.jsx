import React from 'react';

import { GamePage } from './game-page/game-page';
import {
  buildConnection,
  startConnection,
  receiveUserInfo,
  receiveUsersInfo,
  onDisconnection,
} from '../api/connection.api';
import { onPlayerJoin, receiveJoiningPlayerInfo } from '../api/join.api';
import { receiveResetMessage } from '../api/reset.api';
import { onGameCreated, onGameStart } from '../api/start.api';
import axios from 'axios';

export class App extends React.Component {
  state = {
    hubConnection: buildConnection(),
    players: [],
    currentPerson: {
      id: null,
      name: '',
    },
    tableNumber: 1,
    hasStarted: false,
    hasCreated: false,
    gameId: null,
  };

  onResetClick = () => {
    this.setState({hasStarted:false, hasCreated:false})
    this.state.hubConnection.invoke('ResetTable', this.state.tableNumber);     
  }

  render() {
    return (
      <>
        <GamePage
          hubConnection={this.state.hubConnection}
          gameId={this.state.gameId}
          hasStarted={this.state.hasStarted}
          hasCreated={this.state.hasCreated}
          startTheGame={() =>
            this.startTheGame(
              this.state.hubConnection,
              this.state.gameId,
              this.state.currentPerson.id,
            )
          }
          createTheGame={() => this.createTheGame(this.state.hubConnection)}
          tableNumber={this.state.tableNumber}
          players={this.state.players}
          currentPerson={this.state.currentPerson}
          onPlayerJoin={onPlayerJoin(this.state.hubConnection).bind(this)}
          onResetClick={() => this.onResetClick()}
        />
      </>
    );
  }

  createTheGame = (hubConnection) => {
    hubConnection.invoke(
      'CreateGame',
      this.state.tableNumber,
      this.state.currentPerson.id,
    );
  };

  startTheGame = (hubConnection, gameId, userId) => {
    hubConnection.invoke('StartGame', gameId, userId);
  };

  componentDidMount() {
    axios
      .get('https://localhost:44384/api/table/' + this.state.tableNumber)
      .then((result) => {
        this.setState({
          players: [
            ...this.state.players,
            { username: result.data.playerBlackUsername, color: 2 },
            { username: result.data.playerWhiteUsername, color: 1 },
          ],
          gameId: result.data.gameId,
          hasStarted: result.data.gameStarted,
        });
      })
      .catch((err) => console.log(err));

    startConnection(this.state.hubConnection);

    receiveUserInfo(this.state.hubConnection, (id, username) => {
      this.setState({ currentPerson: { id, username } });
    });

    receiveUsersInfo(this.state.hubConnection, (username) =>
      console.log('User ' + username +' joined to table '+this.state.tableNumber)
    );

    receiveJoiningPlayerInfo(
      this.state.hubConnection,
      (tableNumber, username, color) => {
        if (this.state.players.length < 2) {
          this.setState({
            currentPerson: { ...this.state.currentPerson, color },
            players: [
              ...this.state.players,
              { ...this.state.currentPerson, color },
            ],
          });

          [...document.querySelectorAll('span[color]')]
            .filter(
              (nickname) =>
                nickname.attributes.color.value === color.toString(),
            )
            .map((nickname) => (nickname.innerHTML = username));

          console.log(`::receiveJoiningPlayerInfo has returned ${username}`);
        }

        if (this.state.players.some((player) => player.username === username)) {
          document
            .querySelectorAll('button[color]')
            .forEach((button) => (button.style.visibility = 'hidden'));
        }
      },
    );

    receiveResetMessage(this.state.hubConnection, () => {
      this.setState({ players: [] });
      document
        .querySelectorAll('button[color]')
        .forEach((button) => (button.style.visibility = ''));
      console.log(`::receiveResetMessage has been reset.`);

      document.querySelectorAll('span[color]')[0].innerHTML = 'first_player';
      document.querySelectorAll('span[color]')[1].innerHTML = 'second_player';
    });

    onGameCreated(this.state.hubConnection, (gameId) => {
      console.log('Game ' + gameId + ' created!');
      this.setState({ gameId, hasCreated: true });
    });

    onDisconnection(this.state.hubConnection, (msg) =>
      console.log('Haloooo' + msg),
    );

    onGameStart(this.state.hubConnection, () => {
      console.log('Game ' + this.state.gameId + ' started!');
      this.setState({ hasStarted: true });
    });
  }
}
