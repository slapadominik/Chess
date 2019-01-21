export function receiveResetMessage(hubConnection, callback) {
  hubConnection.on('ResetTable_result', callback);
  hubConnection.on('ResetTable_result', (err) =>
    console.log(`::receiveResetMessage received an error ${err}`),
  );
}
