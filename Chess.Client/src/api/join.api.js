export function onPlayerJoin(hubConnection) {
  return function(event) {
    hubConnection.invoke(
      'JoinGame',
      this.state.tableNumber,
      this.state.currentPerson.id,
      event.target.attributes.color.value,
    );
  };
}

export function receiveJoiningPlayerInfo(hubConnection, callback) {
  hubConnection.on('JoinGame_Result', callback);
  hubConnection.on('JoinGame_Error', (err) =>
    console.log(`::receiveJoiningPlayerInfo retrieved an error ${err}`),
  );
}
