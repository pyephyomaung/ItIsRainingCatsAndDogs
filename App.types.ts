import {NativeTouchEvent} from 'react-native';
import {GameEngine} from 'react-native-game-engine';

export interface IGameEngine extends GameEngine {
  swap: Function,
  dispatch: Function
};

export interface IGameEngineEvent {
  type: 'started' | 'ended' | 'swapped' | 'game-over'
};

export interface IDefaultTouchProcessorEvent {
  id: number;
  type: 'start' | 'move' | 'end',
  event: NativeTouchEvent
};

export interface IDefaultTouchProcessorMoveEvent extends IDefaultTouchProcessorEvent {
  type: 'move',
  delta: {
    locationX: number,
    locationY: number,
    pageX: number,
    pageY: number,
    timestamp: number
  }
}