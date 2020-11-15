import React from 'react';
import {Image, ImageStyle, ImageSourcePropType} from 'react-native';
import Matter from 'matter-js';
import {RainProps} from './rain.types';

const RainFactory: (source: ImageSourcePropType) => React.FC<RainProps> = source => props => {
  const width = props.size[0];
  const height = props.size[1];
  const x = props.body.position.x - width / 2;
  const y = props.body.position.y - height / 2;
  const style = {
    position: 'absolute',
    left: x,
    top: y,
    width: width,
    height: height,
  } as ImageStyle;

  return <Image style={style} resizeMode='stretch' source={source}/>;
};

const Rains = [
  RainFactory(require('../../assets/sprites/chibiInu01-scaled.png')),
  RainFactory(require('../../assets/sprites/chibiInu02-scaled.png')),
  RainFactory(require('../../assets/sprites/chibiInu03-scaled.png')),
  RainFactory(require('../../assets/sprites/chibiInu04-scaled.png')),
  RainFactory(require('../../assets/sprites/chibiNeko01-scaled.png')),
  RainFactory(require('../../assets/sprites/chibiNeko02-scaled.png')),
  RainFactory(require('../../assets/sprites/chibiNeko03-scaled.png')),
  RainFactory(require('../../assets/sprites/chibiNeko04-scaled.png'))
];

export const getRandomRain = () => {
  const index = Math.floor(Math.random() * Rains.length);
  return Rains[index];
};