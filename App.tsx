import React, {useRef, useState} from 'react';
import {Image, StatusBar, StyleSheet, View, Text, TouchableOpacity} from 'react-native';
import getEntities from './src/entities';
import Systems from './src/systems';
import Images from './src/assets/images';
import {SCREEN_WIDTH, SCREEN_HEIGHT} from './src/constants';
import {GameEngine, GameEngineProperties} from 'react-native-game-engine';
import {IGameEngine, IGameEngineEvent}  from './App.types';


const App: React.FC<{}> = props => {
  const gameEngineRef = useRef<IGameEngine>(null);
  const [isRunning, setIsRunning] = useState(true);
  const [score, setScore] = useState(0);

  const onEvent = (e: IGameEngineEvent) => {
    switch (e.type) {
      case 'game-over':
        //  setIsRunning(false);
        break;
      case 'score':
        setScore(score + 1);
        break;
    }
  };

  const reset = () => {
    gameEngineRef?.current?.swap(getEntities(gameEngineRef));
    setIsRunning(true);
    setScore(0);
  };

  return (
    <View style={styles.container}>
      <Image 
        source={Images.backgroundSF} 
        style={styles.backgroundImage} 
        resizeMode='cover'/>
      <GameEngine 
        ref={gameEngineRef}
        style={styles.gameContainer}
        entities={getEntities(gameEngineRef)}
        systems={Systems}
        running={isRunning}
        onEvent={onEvent}/>
      <Text style={styles.score}>{score}</Text>
      {!isRunning && (
        <TouchableOpacity style={styles.fullScreenButton} onPress={reset}>
          <View style={styles.fullScreen}>
            <Text style={styles.gameOverText}>Game Over</Text>
            <Text style={styles.gameOverSubText}>Try Again</Text>
          </View>
        </TouchableOpacity>
      )}
      <StatusBar hidden={true}/>
    </View>
  );
};

export default App;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#000'
  },
  backgroundImage: {
    position: 'absolute',
    top: 0,
    bottom: 0,
    left: 0,
    right: 0,
    width: SCREEN_WIDTH,
    height: SCREEN_HEIGHT
  },
  gameContainer: {
    position: 'absolute',
    top: 0,
    bottom: 0,
    left: 0,
    right: 0
  },
  gameOverText: {
    color: 'white',
    fontSize: 48
  },
  gameOverSubText: {
    color: 'white',
    fontSize: 24
  },
  fullScreen: {
    position: 'absolute',
    top: 0,
    bottom: 0,
    left: 0,
    right: 0,
    backgroundColor: 'black',
    opacity: 0.8,
    justifyContent: 'center',
    alignItems: 'center'
  },
  fullScreenButton: {
    position: 'absolute',
    top: 0,
    bottom: 0,
    left: 0,
    right: 0,
    flex: 1
  },
  score: {
    position: 'absolute',
    color: 'white',
    fontSize: 36,
    top: 20,
    left: SCREEN_WIDTH / 2 - 20,
    textShadowColor: '#444444',
    textShadowOffset: {width: 2, height: 2},
    textShadowRadius: 2,
    fontFamily: 'monospace'
  }
});