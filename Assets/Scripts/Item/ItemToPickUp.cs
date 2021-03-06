﻿using Assets.Scripts.Item.Interfaces;
using Functional;
using System;
using UnityEngine;

namespace Assets.Scripts.Item
{
    class ItemToPickUp : MonoBehaviour, ICanBePickedUp
    {
        private readonly Rigidbody _RigidBody;
        private Option<PlayerController> _carryingPlayer = Option.None<PlayerController>();

        public bool CanBePickedUp => !_carryingPlayer.HasValue();
        public bool IsPickedUp => _carryingPlayer.HasValue();

        public ItemToPickUp()
        {
            _RigidBody = GetComponent<Rigidbody>();
        }

        public void OnDrop()
        {
            _carryingPlayer = Option.None<PlayerController>();
        }

        public void OnPickUp(PlayerController player)
        {
            _carryingPlayer = Option.Some(player);
        }

        void Update()
        {
            _carryingPlayer.Do(
                p =>
                {
                    //_RigidBody.velocity = Vector3.zero;
                    //_RigidBody.angularVelocity = Vector3.zero;
                    //transform.SetParent(p.transform);
                    transform.position = p.transform.position;
                },
                () =>
                {
                    transform.SetParent(null);
                }
            );;
        }
    }
}