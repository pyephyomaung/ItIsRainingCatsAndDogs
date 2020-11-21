import {NativeTouchEvent} from 'react-native';
import Matter from 'matter-js';
import {
  IDefaultTouchProcessorEvent,
  IDefaultTouchProcessorMoveEvent
} from '../../../App.types';
import {SCREEN_WIDTH} from '../../constants';

const X_GAP_THRESHOLD = 100;

const UpdateBalloon = (
  entities: any, 
  {touches}: {touches: IDefaultTouchProcessorEvent[]}
) => {
  const moveTouch = touches.find(t => t.type === 'move') as IDefaultTouchProcessorMoveEvent;
  if (moveTouch) {
  
    const touchX = moveTouch.event.pageX;
    const movedX = entities.Balloon.body.position.x + moveTouch.delta.pageX;
    const movedY = entities.Balloon.body.position.y + moveTouch.delta.pageY;
    Matter.Body.setPosition(entities.Balloon.body, {x: touchX, y: movedY});
    
    const gap = touchX - entities.Balloon.body.position.x;
    if (Math.abs(gap) > X_GAP_THRESHOLD) {
      /* Matter.Body.setStatic(entities.Balloon.body, false); */
      Matter.Body.setPosition(entities.Balloon.body, {x: movedX, y: movedY});
/*       Matter.Body.setVelocity(entities.Balloon.body, {x: gap > 0 ? 10 : -10, y: 0}); */
    } else {
      Matter.Body.setVelocity(entities.Balloon.body, {x: 0, y: 0});

      Matter.Body.setStatic(entities.Balloon.body, true);
    }
    
  }

  return entities;
};

export default UpdateBalloon;