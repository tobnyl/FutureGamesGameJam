using UnityEngine;
using UE = UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class Layers
{	
	public static readonly PhysicsLayer Spaceship = new PhysicsLayer("Spaceship");
	public static readonly PhysicsLayer Laser = new PhysicsLayer("Laser");
    public static readonly PhysicsLayer Moon = new PhysicsLayer("Moon");
    public static readonly PhysicsLayer Asteroid = new PhysicsLayer("Asteroid");
    public static readonly PhysicsLayer Chunk = new PhysicsLayer("Chunk");
}

public struct PhysicsLayer
{
	public readonly string Name;
	public readonly int Index;
	public readonly int Mask;

	public PhysicsLayer(string name)
	{
		this.Name = name;
		this.Index = LayerMask.NameToLayer(name);
		this.Mask = 1 << this.Index;
	}
}