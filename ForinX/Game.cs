﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ForinX;

public class Game
{
    private const int Width = 1920;
    private const int Height = 1080;
    private const string Title = "ForinX";

    private readonly RenderWindow _window;

    private readonly Sprite _player;

    public Game()
    {
        _window = new RenderWindow(new VideoMode(Width, Height), Title);
        _window.Closed += (_, _) => _window.Close();

        _player = new Sprite(new Texture("../../../Images/ahri.png"));
        _player.Position = new Vector2f(100f, 100f);
        _player.Scale = new Vector2f(0.25f, 0.25f);
    }

    public void Run()
    {
        var clock = new Clock();
        var timePerFrame = Time.FromSeconds(1f / 120f);
        var timeSinceLastUpdate = Time.Zero;

        while (_window.IsOpen)
        {
            ProcessEvents();

            timeSinceLastUpdate += clock.Restart();
            while (timeSinceLastUpdate > timePerFrame)
            {
                timeSinceLastUpdate -= timePerFrame;

                Update(timePerFrame);
            }

            Render();
        }
    }

    private void ProcessEvents()
    {
        _window.DispatchEvents();

        _moveLeft = _moveRight = _moveUp = _moveDown = false;
        if (Keyboard.IsKeyPressed(Keyboard.Key.W)) _moveUp = true;
        if (Keyboard.IsKeyPressed(Keyboard.Key.S)) _moveDown = true;
        if (Keyboard.IsKeyPressed(Keyboard.Key.A)) _moveLeft = true;
        if (Keyboard.IsKeyPressed(Keyboard.Key.D)) _moveRight = true;
    }

    private void Update(Time dt)
    {
        var movement = new Vector2f(0f, 0f);
        const float speed = 500f;

        if (_moveUp)
            movement.Y -= speed;

        if (_moveDown)
            movement.Y += speed;

        if (_moveLeft)
            movement.X -= speed;

        if (_moveRight)
            movement.X += speed;

        if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            _window.Close();

        _player.Position += movement * dt.AsSeconds();
    }

    private void Render()
    {
        _window.Clear();
        _window.Draw(_player);
        _window.Display();
    }

    private bool _moveLeft;
    private bool _moveRight;
    private bool _moveUp;
    private bool _moveDown;
}