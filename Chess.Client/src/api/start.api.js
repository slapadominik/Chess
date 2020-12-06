export function onGameCreated(hubConnection, callback) {
  hubConnection.on('CreateGame_Result', callback);
}

export function onGameStart(hubConnection, callback) {
  hubConnection.on('StartGame_Result', callback);
}
