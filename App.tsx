import React, {useRef, useState} from 'react';
import {Image, StatusBar, StyleSheet, View, Text, TouchableOpacity} from 'react-native';
import getEntities from './src/entities';
import Systems from './src/systems';
import Images from './src/assets/images';
import {SCREEN_WIDTH, SCREEN_HEIGHT} from './src/constants';
import {GameEngine, GameEngineProperties} from 'react-native-game-engine';
import {IGameEngine, IGameEngineEvent}  from './App.types';

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
  }
});

const App: React.FC<{}> = props => {
  const gameEngineRef = useRef<IGameEngine>(null);
  const [isRunning, setIsRunning] = useState(true);

  const onEvent = (e: IGameEngineEvent) => {
    if (e.type === 'game-over') {
      setIsRunning(false);
    }
  };

  const reset = () => {
    gameEngineRef?.current?.swap(getEntities(gameEngineRef));
    setIsRunning(true);
  };

  return (
    <View style={styles.container}>
      <Image 
        source={Images.backgroundSF} 
        style={styles.backgroundImage} 
        resizeMode="stretch"/>
      <GameEngine 
        ref={gameEngineRef}
        style={styles.gameContainer}
        entities={getEntities(gameEngineRef)}
        systems={Systems}
        running={isRunning}
        onEvent={onEvent}/>
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