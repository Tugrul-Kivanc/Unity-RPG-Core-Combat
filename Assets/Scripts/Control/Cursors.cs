﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class Cursors : MonoBehaviour
    {
        [SerializeField] private CursorType combatCursor;
        [SerializeField] private CursorType movementCursor;
        [SerializeField] private CursorType noneCursor;
        [SerializeField] private CursorType uiCursor;
        public CursorType CombatCursor => combatCursor;
        public CursorType MovementCursor => movementCursor;
        public CursorType NoneCursor => noneCursor;
        public CursorType UICursor => uiCursor;
    }
}