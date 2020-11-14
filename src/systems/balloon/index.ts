import {NativeTouchEvent} from 'react-native';
import Matter from 'matter-js';

const UpdateBalloon = (entities: any, {touches, time}) => {
  const engine = entities.physics.engine;
  const moveTouch = touches.find(t => t.type === 'move');
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