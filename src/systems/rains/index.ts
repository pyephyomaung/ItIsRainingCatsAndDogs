import Matter from 'matter-js';
import {
  TICK_INTERVAL, 
  CELL_X_OFFSET, 
  SCREEN_WIDTH,
  SCREEN_HEIGHT,
  RAIN_RADIUS,
  RAIN_SPAWN_Y_MAX
} from '../../constants';
import Rain from '../../components/rain';
import patterns from './patterns';
import {Entity, Entities} from '../../entities/entities.types';

const Rains = (
  entities: Entities, 
  {time, dispatch}: {time: {delta: number}, dispatch: Function}
) => {
  const engine: Matter.Engine = entities.physics.engine;
  const world: Matter.World = entities.physics.world;
  const tick: number = entities.physics.rainState.tick;
  const patternIndex: number = entities.physics.rainState.patternIndex;

  let isScored = false;
  let hasRain = false;
  Object.keys(entities).forEach(k => {
    const entity = k.startsWith('Rain') ? entities[k] as Entity : null;
    if (entity && entity.body.position.y > SCREEN_HEIGHT) {
      Matter.World.remove(world, entity.body);
      delete(entities[k]);
      isScored = true;
    }

    // also detect rain in entities to help detect end of the game
    entity && (hasRain = true);
  });
  isScored && dispatch({type: 'score'});

  // if no rain left on screen and patterns end, the game ends
  if (patternIndex >= patterns.length && !hasRain) {
    dispatch({type: 'ended'});
  } else if (tick % TICK_INTERVAL === 0 && patternIndex < patterns.length) {
    const pattern = patterns[patternIndex];
    pattern.split('').forEach((char, index) => {
    
      if (char === '1' || char === 'x') {
        const x = CELL_X_OFFSET + index * (SCREEN_WIDTH / pattern.length);
        const y = Math.floor(Math.random() * RAIN_SPAWN_Y_MAX);
        const radius = RAIN_RADIUS;
        entities[`Rain${patternIndex}_${index}`] = Rain(world, x, y, radius);
      }
    });

    entities.physics.rainState.patternIndex += 1;
  }

  Matter.Engine.update(engine, time.delta);
  entities.physics.rainState.tick += 1;

  return entities;
};

export default Rains;