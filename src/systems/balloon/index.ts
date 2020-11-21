import {NativeTouchEvent, TouchableWithoutFeedback} from 'react-native';
import Matter from 'matter-js';
import {
  IDefaultTouchProcessorEvent,
  IDefaultTouchProcessorMoveEvent
} from '../../../App.types';
import {SCREEN_WIDTH, SCREEN_HEIGHT} from '../../constants';

const UpdateBalloon = (
  entities: any, 
  {touches}: {touches: IDefaultTouchProcessorEvent[]}
) => {
  let newX = entities.Balloon.body.position.x;
  let newY = entities.Balloon.body.position.y;
  touches.forEach((t) => {
      const touch = t as IDefaultTouchProcessorMoveEvent;
      if (touch.type === 'move') {
        // for each move touch, update position delta of the balloon
        newX = newX + touch.delta.pageX;
        newY = newY + touch.delta.pageY;
      }
    });

  // make sure the new position are within the screen
  newX = Math.max(Math.min(newX, SCREEN_WIDTH), 0);
  newY = Math.max(Math.min(newX, SCREEN_HEIGHT), 0);
  Matter.Body.setPosition(entities.Balloon.body, {x: newX, y: newY});

  return entities;
};

export default UpdateBalloon;