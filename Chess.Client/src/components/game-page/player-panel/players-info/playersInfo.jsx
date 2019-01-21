import React from 'react';

import * as S from './players-info.styled';

export class PlayersInfo extends React.Component {
  render() {
    return (
      <S.PlayersInfo>
        <div className={'players-info'}>
          <div className={'player-info'}>
            <span color={1}>first_player</span>
            {this.props.players.some(
              (player) => player.color === 1 && player.username !== null,
            ) ? (
              ''
            ) : (
              <button onClick={this.props.onPlayerJoin} color={1}>
                Join as white
              </button>
            )}
          </div>
          <div className={'player-info'}>
            <span color={2}>second_player</span>
            {this.props.players.some(
              (player) => player.color === 2 && player.username !== null,
            ) ? (
              ''
            ) : (
              <button onClick={this.props.onPlayerJoin} color={2}>
                Join as black
              </button>
            )}
          </div>
        </div>
      </S.PlayersInfo>
    );
  }
}
