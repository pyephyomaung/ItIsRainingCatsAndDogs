import Balloon from '../components/balloon';
import Matter from 'matter-js';
import {
  BALLOON_RADIUS, 
  GRAVITY_Y,
  SCREEN_WIDTH, 
  SCREEN_HEIGHT
} from '../constants';

// overwritting this function because the original references HTMLElement
Matter.Common.isElement = () => false;

export default gameEngineRef => {
  const engine = Matter.Engine.create({enableSleeping: false});
  const world = engine.world;
  world.gravity.y = GRAVITY_Y;
  const rainState = {tick: 0, patternIndex: 0};

  Matter.Events.on(engine, 'collisionStart', (event) => {
    const pairs = event.pairs;
    const isBalloonPop = pairs.some(pair => (
      (pair.bodyA.label === 'balloon' && pair.bodyB.label === 'rain') ||
      (pair.bodyA.label === 'rain' && pair.bodyB.label === 'balloon')
    ));

    if (isBalloonPop) {
      gameEngineRef.current.dispatch({ type: "game-over"});
    }
  });

  return {
    physics: {engine, world, rainState},
    Balloon: Balloon(world, 'red', SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2, BALLOON_RADIUS)
  };
};