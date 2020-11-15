import React from 'react';

export type Entity = {
  body: Matter.Body;
  size: number[];
  renderer: React.FC<any>;
}

export type Physics = {
  engine: any;
  world: any;
  rainState: any;
}

export type Entities = {
  [x: string]: Entity | Physics,
  physics: Physics
};