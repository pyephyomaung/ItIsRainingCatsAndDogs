import React from 'react';
import {Image, ImageStyle, ImageSourcePropType} from 'react-native';
import Images from '../../assets/images';
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

const Rains = Object.values(Images.cats)
  .concat(Object.values(Images.dogs))
  .map(RainFactory);

export const getRandomRain = () => {
  const index = Math.floor(Math.random() * Rains.length);
  return Rains[index];
};