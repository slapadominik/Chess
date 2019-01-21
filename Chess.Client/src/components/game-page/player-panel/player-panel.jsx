import React from 'react';

import * as S from './player-panel.styled';
import { PlayersInfo } from './players-info/playersInfo';
import {
  getWaitingForPlayersMessage,
  getEndGameMessage,
} from './player-panel.utils';

export class PlayerPanel extends React.Component {
  render() {
    return (
      <S.PlayerPanel>
        <div className="player-panel">
          <div className="game-info">
            Table #{this.props.tableNumber}.
            <br />
            {getWaitingForPlayersMessage(this.props.players.length)}
            {getEndGameMessage(this.props.isFinished, this.props.winner)}
          </div>
          <div className="player-info">
            <PlayersInfo
              onPlayerJoin={this.props.onPlayerJoin}
              players={this.props.players}
            />
          </div>
          <div className="actions">
            <button onClick={this.props.onResetClick}>Reset players</button>
            <button
              disabled={this.props.hasCreated}
              onClick={this.props.createTheGame}
            >
              Create
            </button>
            <button
              disabled={!this.props.hasCreated || this.props.hasStarted}
              onClick={this.props.startTheGame}
            >
              Start
            </button>
          </div>
          <div className="chatroom">
            <textarea className="chat" />
          </div>
        </div>
      </S.PlayerPanel>
    );
  }
}
