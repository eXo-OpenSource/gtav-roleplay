import * as alt from 'alt-client';
import * as native from 'natives';
import { Vector3 } from "alt-client";

// Get the distance between two vectors.
export function distance(vector1: Vector3, vector2: Vector3): number {
  if (vector1 === undefined || vector2 === undefined) {
    throw new Error('AddVector => vector1 or vector2 is undefined');
  }

  return Math.sqrt(
    Math.pow(vector1.x - vector2.x, 2) +
      Math.pow(vector1.y - vector2.y, 2) +
      Math.pow(vector1.z - vector2.z, 2)
  );
}

// Get a random position based around.
export function randPosAround(pos: Vector3, range: number) : Vector3 {
  return new Vector3(
    pos.x + Math.random() * (range * 2) - range,
    pos.y + Math.random() * (range * 2) - range,
    pos.z
  );
}

export function getForwardVector(entity: number, distance: number): Vector3 {
  const forward = native.getEntityForwardVector(entity);
  const pos = native.getEntityCoords(entity, false);
  return new Vector3(
    pos.x + forward.x * distance,
    pos.y + forward.y * distance,
    pos.z
);
}
