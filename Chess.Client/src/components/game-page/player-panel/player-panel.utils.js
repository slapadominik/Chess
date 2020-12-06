export function getWaitingForPlayersMessage(numberOfPlayers) {
  if (numberOfPlayers === 0) return 'Waiting for both players to join.';
  else if (numberOfPlayers === 1) return 'Waiting for a player to join.';
  return '';
}

export function getEndGameMessage(isFinished, winner) {
  return isFinished ? `${winner} is the winner!` : '';
}
