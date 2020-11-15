import {NativeTouchEvent} from 'react-native';
import Matter from 'matter-js';
import {
  IDefaultTouchProcessorEvent,
  IDefaultTouchProcessorMoveEvent
} from '../../../App.types';

const UpdateBalloon = (
  entities: any, 
  {touches}: {touches: IDefaultTouchProcessorEvent[]}
) => {
  const engine = entities.physics.engine;
  const moveTouch = touches.find(t => t.type === 'move') as IDefaultTouchProcessorMoveEvent;
  if (moveTouch) {
    Matter.Body.setPosition(
      entities.Balloon.body, 
      Matter.Vector.create(
        entities.Balloon.body.position.x + moveTouch.delta.pageX,
        entities.Balloon.body.position.y + moveTouch.delta.pageY
      )
    );
  }

  return entities;
};

export default UpdateBalloon;