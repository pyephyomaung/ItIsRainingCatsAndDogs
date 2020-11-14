import React from 'react';
import Matter from 'matter-js';
import Balloon from './balloon';

export default (
  world: Matter.World,
  color: string, 
  x: number, 
  y: number,
  radius: number
) => {
  const initialBalloon = Matter.Bodies.circle(
    x,
    y,
    radius,
    {isStatic: true, label: 'balloon'}
  );

  Matter.World.add(world, [initialBalloon]);

  return {
    body: initialBalloon,
    size: [radius * 2, radius * 2],
    color: color,
    renderer: <Balloon/>
  };
};