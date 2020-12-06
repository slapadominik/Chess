import styled from 'styled-components';

export const PlayerPanel = styled.div`
  .player-panel {
    background-color: #edf4ff;
    width: 300px;
    height: 500px;
    font-size: 20px;
    line-height: 1.6rem;
    font-weight: 700;

    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: center;
  }

  .game-info {
    margin-top: 30px;
    text-align: center;
  }

  .chat {
    margin-bottom: 20px;
    width: 250px;
    height: 200px;
  }
`;
