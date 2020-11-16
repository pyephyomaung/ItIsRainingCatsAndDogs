import React from 'react';
import {Image} from 'react-native';
import Images from '../../assets/images';
import {BalloonProps} from './balloon.types';

const Balloon: React.FC<BalloonProps> = props => {
  const width = props.size[0];
  const height = props.size[1];
  const x = props.body.position.x - width / 2;
  const y = props.body.position.y - height / 2;
  return (
    <Image
      style={{
        position: 'absolute',
        left: x,
        top: y,
        width: width,
        height: height,
      }}
      resizeMode='stretch'
      source={Images.balloon}
    />
  );
};

export default Balloon;