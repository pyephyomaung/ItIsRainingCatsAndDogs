import React from 'react';
import Matter from 'matter-js';
import {getRandomRain} from './rain';

export default (
  world: Matter.World,
  x: number, 
  y: number,
  radius: number
) => {
  const initialRain = Matter.Bodies.circle(
    x,
    y,
    radius,
    {label: 'rain'}
  );

  Matter.World.add(world, [initialRain]);

  return {
    body: initialRain,
    size: [radius * 2, radius * 2],
    renderer: getRandomRain()
  };
};