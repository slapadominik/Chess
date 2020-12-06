export const isPersonAlreadyPlayer = (players, currentPerson) => {
  // console.log(players);
  // console.log(currentPerson.name);

  return players.some(
    (person) => JSON.stringify(person) === JSON.stringify(currentPerson),
  );
};
