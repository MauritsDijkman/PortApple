using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{
    bool _stepped = false;
    public bool _paused = false;
    int _stepIndex = 0;
    int _startSceneNumber = 0;

    bool _spawnBall = false;

    bool startHoverSoundHasPlayed = false;
    bool controlsHoverSoundHasPlayed = false;
    bool quitHoverSoundHasPlayed = false;
    bool backHoverSoundHasPlayed = false;

    public bool nextLevel1 = false;
    public bool nextLevel2 = false;
    public bool nextLevel3 = false;
    public bool nextLevel4 = false;
    public bool startTransition = false;

    public bool menuIsActive = false;
    public bool level1Active = false;
    public bool level2Active = false;
    public bool level3Active = false;
    public bool level4Active = false;

    public int numberOfBalls = 0;

    bool controlMenuIsActive = false;

    Canvas _lineContainer = null;

    public List<Ball> _ball;
    public List<ScoreBall> _scoreball;
    public List<LineSegment> _lines;

    public PortalLine _portalBlueLineInLevel1;
    public PortalLine _portalBlueLineOutLevel1;
    public PortalLine _portalBlueLineInLevel2;
    public PortalLine _portalBlueLineOutLevel2;
    public PortalLine _portalBlueLineInLevel3;
    public PortalLine _portalBlueLineOutLevel3;
    public PortalLine _portalBlueLineInLevel4;
    public PortalLine _portalBlueLineOutLevel4;

    public Portal_Image _portalBlueImageInLevel1;
    public Portal_Image _portalBlueImageOutLevel1;
    public Portal_Image _portalBlueImageInLevel2;
    public Portal_Image _portalBlueImageOutLevel2;
    public Portal_Image _portalBlueImageInLevel3;
    public Portal_Image _portalBlueImageOutLevel3;
    public Portal_Image _portalBlueImageInLevel4;
    public Portal_Image _portalBlueImageOutLevel4;

    public PortalLine _portalOrangeLine1Level1;
    public PortalLine _portalOrangeLine2Level1;
    public Portal_Image _portalOrangeImageInLevel1;
    public Portal_Image _portalOrangeImageOutLevel1;

    public PortalLine _portalOrangeLine1Level2;
    public PortalLine _portalOrangeLine2Level2;
    public Portal_Image _portalOrangeImageInLevel2;
    public Portal_Image _portalOrangeImageOutLevel2;

    public PortalLine _portalOrangeLine1Level3;
    public PortalLine _portalOrangeLine2Level3;
    public Portal_Image _portalOrangeImageInLevel3;
    public Portal_Image _portalOrangeImageOutLevel3;

    public PortalLine _portalOrangeLine1Level4;
    public PortalLine _portalOrangeLine2Level4;
    public Portal_Image _portalOrangeImageInLevel4;
    public Portal_Image _portalOrangeImageOutLevel4;

    public Fruit_Image _appleimage;
    RotationArrow _rotationArrow;

    Backgrounds _startMenu;
    Backgrounds _pauseMenu;
    Backgrounds _controlsMenu;
    Backgrounds _background;
    Backgrounds _level1;
    Backgrounds _level2;
    Backgrounds _level3;
    Backgrounds _level4;

    Backgrounds _backgroundInstruction1;
    Backgrounds _backgroundInstruction2;
    Backgrounds _backgroundInstruction3;
    Backgrounds _backgroundInstruction4;

    Buttons _startButton;
    Buttons _startButton_Hover;
    Buttons _controlsButton;
    Buttons _controlsButton_Hover;
    Buttons _quitButton;
    Buttons _quitButton_Hover;
    Buttons _backButton;
    Buttons _backButton_Hover;

    RotatableTriangle_Image _rotatableTriangleImageLevel3;
    RotatableTriangle_Image _rotatableTriangleImageLevel4;

    Platform_Image _platform1;
    Platform_Image _platform2;
    Platform_Image _platform3;
    Platform_Image _platform4;
    Platform_Image _platform5;
    Platform_Image _platform6;
    Platform_Image _platform7;
    Platform_Image _platform8;
    Platform_Image _platform9;
    Platform_Image _platform10;
    Platform_Image _platform11;
    Platform_Image _platform12;
    Platform_Image _platform13;
    Platform_Image _platform14;
    Platform_Image _platform15;
    Platform_Image _platform16;

    public Blender _blenderLevel1;
    public Blender _blenderLevel2;
    public Blender _blenderLevel3;
    public Blender _blenderLevel4;

    public BlenderLine _blenderLine1;
    public BlenderLine _blenderLine2;
    public BlenderLine _blenderLine3;
    public BlenderLine _blenderLine4;

    Instruction1 _instructions1;
    Instruction2 _instructions2;
    Instruction3 _instructions3;
    Instruction4 _instructions4;

    public LevelTransition _levelTransition;

    Score_HUD _HUD;

    Vec2 ballSpawnPosition;

    public int score;

    int fruitImage;

    Sound _hoverSound;

    Sound _backgroundMusic;
    SoundChannel _backgroundMusicChannel;

    GameObject levelHolder;


    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      public MyGame() : base(1440, 900, false, false)                                      //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public MyGame() : base(1440, 900, false, false)
    {

        levelHolder = new Level();
        AddChild(levelHolder);

        _hoverSound = new Sound("Hover_Sound.wav", false, false);
        _backgroundMusic = new Sound("Background_Music.mp3", true, false);

        _lineContainer = new Canvas(width, height);
        AddChild(_lineContainer);

        targetFps = 60;

        score = 0;

        startMusic();

        _ball = new List<Ball>();
        _scoreball = new List<ScoreBall>();
        _lines = new List<LineSegment>();

        LoadScene(_startSceneNumber);

        LoadInstruction1();
        LoadInstruction2();
        LoadInstruction3();
        LoadInstruction4();

        LoadLevel1();
        LoadLevel2();
        LoadLevel3();
        LoadLevel4();

        _startButton.visible = true;
        _startButton_Hover.visible = false;

        _controlsButton.visible = true;
        _controlsButton_Hover.visible = false;

        _quitButton.visible = true;
        _quitButton_Hover.visible = false;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      GetNumberOfBalls()                                                                   //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public int GetNumberOfBalls()
    {
        return _ball.Count;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Ball Getball(int index)                                                              //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public Ball GetBall(int index)
    {
        if (index >= 0 && index < _ball.Count)
        {
            return _ball[index];
        }
        return null;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      GetNumberOfLines()                                                                   //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public int GetNumberOfLines()
    {
        return _lines.Count;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      LineSegment GetLine(int index)                                                       //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public LineSegment GetLine(int index)
    {
        if (index >= 0 && index < _lines.Count)
        {
            return _lines[index];
        }
        return null;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      DrawLine(Vec2 start, Vec2 end)                                                       //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public void DrawLine(Vec2 start, Vec2 end)
    {
        _lineContainer.graphics.DrawLine(Pens.White, start.x, start.y, end.x, end.y);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      AddLine(Vec2 start, Vec2 end)                                                        //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public void AddLine(Vec2 start, Vec2 end, LineSegment l = null)
    {
        LineSegment line = l;

        if (l == null)
        {
            //line = new LineSegment(start, end, 0xff00ff00, 4);  // Green line
            line = new LineSegment(start, end, 0x0000ff00, 4);  // Invisible line
        }

        AddChild(line);
        _lines.Add(line);
        _ball.Add(new Ball(0, 0, 0, 0, start, moving: false));
        _ball.Add(new Ball(0, 0, 0, 0, end, moving: false));
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      ResetScene()                                                                         //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void ResetScene()
    {
        _paused = false;
        _stepped = false;
        _spawnBall = false;
        score = 0;

        menuIsActive = false;
        controlMenuIsActive = false;
        level1Active = false;
        level2Active = false;
        level3Active = false;
        level4Active = false;

        numberOfBalls = 0;

        // Remove all the balls
        foreach (Ball ball in _ball)
        {
            ball.LateDestroy();
        }
        _ball.Clear();

        // Remove all the lines
        foreach (LineSegment line in _lines)
        {
            line.LateDestroy();
        }
        _lines.Clear();

        // Remove all scoreball
        foreach (ScoreBall scoreball in _scoreball)
        {
            scoreball.DeleteScoreBall();
        }
        _scoreball.Clear();

        // Be sure the object isn't null
        // The ? has the same purpose as if (_... != null)

        if (_appleimage != null)
            RemoveChild(_appleimage);
        if (_rotationArrow != null)
            RemoveChild(_rotationArrow);

        // Levels
        if (_level1 != null)
            RemoveChild(_level1);
        if (_level2 != null)
            RemoveChild(_level2);
        if (_level3 != null)
            RemoveChild(_level3);
        if (_level4 != null)
            RemoveChild(_level4);

        // Rotatable image
        if (_rotatableTriangleImageLevel3 != null)
            RemoveChild(_rotatableTriangleImageLevel3);
        if (_rotatableTriangleImageLevel4 != null)
            RemoveChild(_rotatableTriangleImageLevel4);

        // Blue portals
        if (_portalBlueImageInLevel1 != null)
            RemoveChild(_portalBlueImageInLevel1);
        if (_portalBlueImageOutLevel1 != null)
            RemoveChild(_portalBlueImageOutLevel1);

        if (_portalBlueImageInLevel2 != null)
            RemoveChild(_portalBlueImageInLevel2);
        if (_portalBlueImageOutLevel2 != null)
            RemoveChild(_portalBlueImageOutLevel2);

        if (_portalBlueImageInLevel3 != null)
            RemoveChild(_portalBlueImageInLevel3);
        if (_portalBlueImageOutLevel3 != null)
            RemoveChild(_portalBlueImageOutLevel3);

        if (_portalBlueImageInLevel4 != null)
            RemoveChild(_portalBlueImageInLevel4);
        if (_portalBlueImageOutLevel4 != null)
            RemoveChild(_portalBlueImageOutLevel4);

        if (_portalBlueLineInLevel1 != null)
            RemoveChild(_portalBlueLineInLevel1);
        if (_portalBlueLineOutLevel1 != null)
            RemoveChild(_portalBlueLineOutLevel1);

        if (_portalBlueLineInLevel2 != null)
            RemoveChild(_portalBlueLineInLevel2);
        if (_portalBlueLineOutLevel2 != null)
            RemoveChild(_portalBlueLineOutLevel2);

        if (_portalBlueLineInLevel3 != null)
            RemoveChild(_portalBlueLineInLevel3);
        if (_portalBlueLineOutLevel3 != null)
            RemoveChild(_portalBlueLineOutLevel3);

        if (_portalBlueLineInLevel4 != null)
            RemoveChild(_portalBlueLineInLevel4);
        if (_portalBlueLineOutLevel4 != null)
            RemoveChild(_portalBlueLineOutLevel4);

        // Orange portals
        if (_portalOrangeImageInLevel1 != null)
            RemoveChild(_portalOrangeImageInLevel1);
        if (_portalOrangeImageOutLevel1 != null)
            RemoveChild(_portalOrangeImageOutLevel1);
        if (_portalOrangeLine1Level1 != null)
            RemoveChild(_portalOrangeLine1Level1);
        if (_portalOrangeLine2Level1 != null)
            RemoveChild(_portalOrangeLine2Level1);

        if (_portalOrangeImageInLevel2 != null)
            RemoveChild(_portalOrangeImageInLevel2);
        if (_portalOrangeImageOutLevel2 != null)
            RemoveChild(_portalOrangeImageOutLevel2);
        if (_portalOrangeLine1Level2 != null)
            RemoveChild(_portalOrangeLine1Level2);
        if (_portalOrangeLine2Level2 != null)
            RemoveChild(_portalOrangeLine2Level2);

        if (_portalOrangeImageInLevel3 != null)
            RemoveChild(_portalOrangeImageInLevel3);
        if (_portalOrangeImageOutLevel3 != null)
            RemoveChild(_portalOrangeImageOutLevel3);
        if (_portalOrangeLine1Level3 != null)
            RemoveChild(_portalOrangeLine1Level3);
        if (_portalOrangeLine2Level3 != null)
            RemoveChild(_portalOrangeLine2Level3);

        if (_portalOrangeImageInLevel4 != null)
            RemoveChild(_portalOrangeImageInLevel4);
        if (_portalOrangeImageOutLevel4 != null)
            RemoveChild(_portalOrangeImageOutLevel4);
        if (_portalOrangeLine1Level4 != null)
            RemoveChild(_portalOrangeLine1Level4);
        if (_portalOrangeLine2Level4 != null)
            RemoveChild(_portalOrangeLine2Level4);

        // Platform
        if (_platform1 != null)
            RemoveChild(_platform1);
        if (_platform2 != null)
            RemoveChild(_platform2);
        if (_platform3 != null)
            RemoveChild(_platform3);
        if (_platform4 != null)
            RemoveChild(_platform4);
        if (_platform5 != null)
            RemoveChild(_platform5);
        if (_platform6 != null)
            RemoveChild(_platform6);
        if (_platform6 != null)
            RemoveChild(_platform6);
        if (_platform7 != null)
            RemoveChild(_platform7);
        if (_platform8 != null)
            RemoveChild(_platform8);
        if (_platform9 != null)
            RemoveChild(_platform9);
        if (_platform10 != null)
            RemoveChild(_platform10);
        if (_platform11 != null)
            RemoveChild(_platform11);
        if (_platform12 != null)
            RemoveChild(_platform12);
        if (_platform13 != null)
            RemoveChild(_platform13);
        if (_platform14 != null)
            RemoveChild(_platform14);
        if (_platform15 != null)
            RemoveChild(_platform15);
        if (_platform16 != null)
            RemoveChild(_platform16);

        //Blender
        if (_blenderLevel1 != null)
            RemoveChild(_blenderLevel1);
        if (_blenderLevel2 != null)
            RemoveChild(_blenderLevel2);
        if (_blenderLevel3 != null)
            RemoveChild(_blenderLevel3);
        if (_blenderLevel4 != null)
            RemoveChild(_blenderLevel4);

        if (_blenderLine1 != null)
            RemoveChild(_blenderLine1);
        if (_blenderLine2 != null)
            RemoveChild(_blenderLine2);
        if (_blenderLine3 != null)
            RemoveChild(_blenderLine3);
        if (_blenderLine4 != null)
            RemoveChild(_blenderLine4);

        // HUD
        if (_HUD != null)
            RemoveChild(_HUD);

        // Pause menu
        if (_pauseMenu != null)
            RemoveChild(_pauseMenu);

        // Menu
        if (_startMenu != null)
            RemoveChild(_startMenu);
        if (_startButton != null)
            RemoveChild(_startButton);
        if (_startButton_Hover != null)
            RemoveChild(_startButton_Hover);
        if (_controlsButton != null)
            RemoveChild(_controlsButton);
        if (_controlsButton_Hover != null)
            RemoveChild(_controlsButton_Hover);
        if (_quitButton != null)
            RemoveChild(_quitButton);
        if (_quitButton_Hover != null)
            RemoveChild(_quitButton_Hover);

        // Instructions
        if (_backgroundInstruction1 != null)
            RemoveChild(_backgroundInstruction1);
        if (_backgroundInstruction2 != null)
            RemoveChild(_backgroundInstruction2);
        if (_backgroundInstruction3 != null)
            RemoveChild(_backgroundInstruction3);
        if (_backgroundInstruction4 != null)
            RemoveChild(_backgroundInstruction4);

        if (_instructions1 != null)
            RemoveChild(_instructions1);
        if (_instructions2 != null)
            RemoveChild(_instructions2);
        if (_instructions3 != null)
            RemoveChild(_instructions3);
        if (_instructions4 != null)
            RemoveChild(_instructions4);
    }

    void LoadInstruction1()
    {
        _backgroundInstruction1 = new Backgrounds(3);
        _instructions1 = new Instruction1();
    }

    void LoadInstruction2()
    {
        _backgroundInstruction2 = new Backgrounds(3);
        _instructions2 = new Instruction2();
    }

    void LoadInstruction3()
    {
        _backgroundInstruction3 = new Backgrounds(3);
        _instructions3 = new Instruction3();
    }

    void LoadInstruction4()
    {
        _backgroundInstruction4 = new Backgrounds(3);
        _instructions4 = new Instruction4();
    }

    void LoadLevel1()
    {
        _level1 = new Backgrounds(4);
        _portalBlueImageInLevel1 = new Portal_Image(new Vec2(400, height - 40), 0, 0);
        _portalBlueImageOutLevel1 = new Portal_Image(new Vec2(920, 600), 0, 180);
        _portalOrangeImageInLevel1 = new Portal_Image(new Vec2(1310, 600), 1, 180);
        _portalOrangeImageOutLevel1 = new Portal_Image(new Vec2(1130, height - 40), 1, 0);
        _blenderLevel1 = new Blender(new Vec2(410, 105.5f), 0, 180);
        _HUD = new Score_HUD();
        _pauseMenu = new Backgrounds(2);
    }

    void LoadLevel2()
    {
        _level2 = new Backgrounds(5);
        _portalBlueImageInLevel2 = new Portal_Image(new Vec2(1100, height - 40), 0, 0);
        _portalBlueImageOutLevel2 = new Portal_Image(new Vec2(150, height - 350), 0, 180);
        _portalOrangeImageInLevel2 = new Portal_Image(new Vec2(650, height - 40), 1, 0);
        _portalOrangeImageOutLevel2 = new Portal_Image(new Vec2(1330, height - 860), 1, 180);
        _blenderLevel2 = new Blender(new Vec2(105.5f, 343), 0, 90);
        _HUD = new Score_HUD();
        _pauseMenu = new Backgrounds(2);
    }

    void LoadLevel3()
    {
        _level3 = new Backgrounds(6);
        _portalBlueImageInLevel3 = new Portal_Image(new Vec2(600, height - 860), 0, 180);
        _portalBlueImageOutLevel3 = new Portal_Image(new Vec2(1330, height - 290), 0, 0);
        _portalOrangeImageInLevel3 = new Portal_Image(new Vec2(600, height - 40), 1, 0);
        _portalOrangeImageOutLevel3 = new Portal_Image(new Vec2(1330, height - 200), 1, 180);
        _blenderLevel3 = new Blender(new Vec2(880, 711), 10, 0);
        _HUD = new Score_HUD();
        _pauseMenu = new Backgrounds(2);
    }

    void LoadLevel4()
    {
        _level4 = new Backgrounds(7);
        _portalBlueImageInLevel4 = new Portal_Image(new Vec2(500, height - 40), 0, 0);
        _portalBlueImageOutLevel4 = new Portal_Image(new Vec2(675, height - 860), 0, 180);
        _portalOrangeImageInLevel4 = new Portal_Image(new Vec2(1315, height - 530), 1, 0);
        _portalOrangeImageOutLevel4 = new Portal_Image(new Vec2(675, height - 230), 1, 180);
        _blenderLevel4 = new Blender(new Vec2(125, 640), 20, 62);
        _HUD = new Score_HUD();
        _pauseMenu = new Backgrounds(2);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      LoadScene(int sceneNumber)                                                           //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public void LoadScene(int sceneNumber)
    {
        _startSceneNumber = sceneNumber;

        ResetScene();

        switch (sceneNumber)
        {
            default:
                menuIsActive = true;

                _startMenu = new Backgrounds(0);
                _startButton = new Buttons(new Vec2(width / 2, height / 2), 0);
                _startButton_Hover = new Buttons(new Vec2(width / 2, height / 2), 4);
                _controlsButton = new Buttons(new Vec2(width / 2, height / 2 + 100), 1);
                _controlsButton_Hover = new Buttons(new Vec2(width / 2, height / 2 + 100), 5);
                _quitButton = new Buttons(new Vec2(width / 2, height / 2 + 200), 2);
                _quitButton_Hover = new Buttons(new Vec2(width / 2, height / 2 + 200), 6);

                AddChild(_startMenu);
                AddChild(_startButton);
                AddChild(_startButton_Hover);
                AddChild(_controlsButton);
                AddChild(_controlsButton_Hover);
                AddChild(_quitButton);
                AddChild(_quitButton_Hover);
                break;


            case 2:
                controlMenuIsActive = true;

                _controlsMenu = new Backgrounds(1);
                _backButton = new Buttons(new Vec2(1070, 855), 3);
                _backButton_Hover = new Buttons(new Vec2(1070, 855), 7);

                levelHolder.AddChild(_controlsMenu);
                levelHolder.AddChild(_backButton);
                levelHolder.AddChild(_backButton_Hover);
                break;


            case 3:
                _spawnBall = true;
                level1Active = true;

                ballSpawnPosition = new Vec2(width / 4 - 100, height - height / 4);
                fruitImage = 0;

                // Border
                AddLine(new Vec2(width - width, height - height), new Vec2(width, height - height));    // Top
                AddLine(new Vec2(width, height), new Vec2(width - width, height));                      // Bottom
                AddLine(new Vec2(width - width, height), new Vec2(width - width, height - height));     // Left
                AddLine(new Vec2(width, height - height), new Vec2(width, height));                     // Right

                //Level
                levelHolder.AddChild(_level1);

                // Lines
                AddLine(new Vec2(0, 505), new Vec2(325, 505));                  // Left object
                AddLine(new Vec2(325, 395), new Vec2(0, 395));
                AddLine(new Vec2(325, 505), new Vec2(325, 395));

                AddLine(new Vec2(500, 395), new Vec2(500, 505));                // Right object appendice
                AddLine(new Vec2(500, 505), new Vec2(700, 505));
                AddLine(new Vec2(700, 395), new Vec2(500, 395));

                AddLine(new Vec2(700, 0), new Vec2(700, 395));                  // Right object
                AddLine(new Vec2(800, 900), new Vec2(800, 0));
                AddLine(new Vec2(700, 505), new Vec2(700, 900));

                AddLine(new Vec2(1100 - 100, 875), new Vec2(940 - 100, 740));   // Left triangle
                AddLine(new Vec2(940 - 100, 875), new Vec2(1100 - 100, 875));
                AddLine(new Vec2(940 - 100, 740), new Vec2(940 - 100, 875));

                AddLine(new Vec2(1410, 740), new Vec2(1250, 875));              // Right triangle
                AddLine(new Vec2(1410, 875), new Vec2(1410, 740));
                AddLine(new Vec2(1250, 875), new Vec2(1410, 875));

                // Blue portal
                _portalBlueLineInLevel1 = new PortalLine(new Vec2(_portalBlueImageInLevel1.x + 100, _portalBlueImageInLevel1.y), new Vec2(_portalBlueImageInLevel1.x - 100, _portalBlueImageInLevel1.y));
                _portalBlueLineOutLevel1 = new PortalLine(new Vec2(_portalBlueImageOutLevel1.x - 100, _portalBlueImageOutLevel1.y), new Vec2(_portalBlueImageOutLevel1.x + 100, _portalBlueImageOutLevel1.y));

                levelHolder.AddChild(_portalBlueImageInLevel1);
                levelHolder.AddChild(_portalBlueImageOutLevel1);
                levelHolder.AddChild(_portalBlueLineInLevel1);
                levelHolder.AddChild(_portalBlueLineOutLevel1);

                // Orange portal
                _portalOrangeLine1Level1 = new PortalLine(new Vec2(_portalOrangeImageInLevel1.x - 100, _portalOrangeImageInLevel1.y), new Vec2(_portalOrangeImageInLevel1.x + 100, _portalOrangeImageInLevel1.y));
                _portalOrangeLine2Level1 = new PortalLine(new Vec2(_portalOrangeImageOutLevel1.x + 100, _portalOrangeImageOutLevel1.y), new Vec2(_portalOrangeImageOutLevel1.x - 100, _portalOrangeImageOutLevel1.y));

                levelHolder.AddChild(_portalOrangeImageInLevel1);
                levelHolder.AddChild(_portalOrangeImageOutLevel1);
                levelHolder.AddChild(_portalOrangeLine1Level1);
                levelHolder.AddChild(_portalOrangeLine2Level1);

                // Platform
                _platform1 = new Platform_Image(new Vec2(420, 300), 0, 0);
                _platform2 = new Platform_Image(new Vec2(1150, 200), 1, 0);

                levelHolder.AddChild(_platform1);
                levelHolder.AddChild(_platform2);

                // Blender                
                _blenderLine1 = new BlenderLine(new Vec2(_blenderLevel1.x - 35, _blenderLevel1.y - 58), new Vec2(_blenderLevel1.x + 35, _blenderLevel1.y - 58));

                levelHolder.AddChild(_blenderLevel1);
                levelHolder.AddChild(_blenderLine1);

                AddLine(new Vec2(_blenderLevel1.x - 52, _blenderLevel1.y + 95), new Vec2(_blenderLevel1.x - 52, _blenderLevel1.y - 12));    // Left  
                AddLine(new Vec2(_blenderLevel1.x + 52, _blenderLevel1.y - 12), new Vec2(_blenderLevel1.x + 52, _blenderLevel1.y + 95));    // Right 
                AddLine(new Vec2(_blenderLevel1.x - 52, _blenderLevel1.y - 12), new Vec2(_blenderLevel1.x - 35, _blenderLevel1.y - 58));    // Left oblique
                AddLine(new Vec2(_blenderLevel1.x + 35, _blenderLevel1.y - 58), new Vec2(_blenderLevel1.x + 52, _blenderLevel1.y - 12));    // Right oblique

                // Score balls
                _scoreball.Add(new ScoreBall(new Vec2(width / 2 + 410, height - 850)));
                _scoreball.Add(new ScoreBall(new Vec2(1125, 775)));
                _scoreball.Add(new ScoreBall(new Vec2(width / 2 - 309, height - 655)));

                // HUD               
                levelHolder.AddChild(_HUD);

                // Pause menu               
                _pauseMenu.visible = false;
                levelHolder.AddChild(_pauseMenu);
                break;


            case 4:
                _spawnBall = true;
                level2Active = true;

                ballSpawnPosition = new Vec2(width - 100, height - 100);
                fruitImage = 0;

                // Border
                AddLine(new Vec2(width - width, height - height), new Vec2(width, height - height));    // Top
                AddLine(new Vec2(width, height), new Vec2(width - width, height));                      // Bottom
                AddLine(new Vec2(width - width, height), new Vec2(width - width, height - height));     // Left
                AddLine(new Vec2(width, height - height), new Vec2(width, height));                     // Right

                // Level                
                levelHolder.AddChild(_level2);

                // Lines
                AddLine(new Vec2(1275, 395), new Vec2(0, 395));     // Top
                AddLine(new Vec2(1440, 255), new Vec2(1275, 395));  // Bottom
                AddLine(new Vec2(0, 490), new Vec2(1440, 490));     // Diagonal top right

                AddLine(new Vec2(60, 825), new Vec2(215, 825));     // Triangle bottom
                AddLine(new Vec2(215, 825), new Vec2(60, 695));     // Triangle diagonal
                AddLine(new Vec2(65, 700), new Vec2(65, 825));      // Triangle left 

                AddLine(new Vec2(965, 900), new Vec2(875, 490));    // Diagonal bottom right 
                AddLine(new Vec2(785, 490), new Vec2(875, 900));    // Diagonal bottom left 

                // Blue portal
                _portalBlueLineInLevel2 = new PortalLine(new Vec2(_portalBlueImageInLevel2.x + 100, _portalBlueImageInLevel2.y), new Vec2(_portalBlueImageInLevel2.x - 100, _portalBlueImageInLevel2.y));
                _portalBlueLineOutLevel2 = new PortalLine(new Vec2(_portalBlueImageOutLevel2.x - 100, _portalBlueImageOutLevel2.y), new Vec2(_portalBlueImageOutLevel2.x + 100, _portalBlueImageOutLevel2.y));

                levelHolder.AddChild(_portalBlueImageInLevel2);
                levelHolder.AddChild(_portalBlueImageOutLevel2);
                levelHolder.AddChild(_portalBlueLineInLevel2);
                levelHolder.AddChild(_portalBlueLineOutLevel2);

                // Orange portal
                _portalOrangeLine1Level2 = new PortalLine(new Vec2(_portalOrangeImageInLevel2.x + 100, _portalOrangeImageInLevel2.y), new Vec2(_portalOrangeImageInLevel2.x - 100, _portalOrangeImageInLevel2.y));
                _portalOrangeLine2Level2 = new PortalLine(new Vec2(_portalOrangeImageOutLevel2.x - 100, _portalOrangeImageOutLevel2.y), new Vec2(_portalOrangeImageOutLevel2.x + 100, _portalOrangeImageOutLevel2.y));

                levelHolder.AddChild(_portalOrangeImageInLevel2);
                levelHolder.AddChild(_portalOrangeImageOutLevel2);
                levelHolder.AddChild(_portalOrangeLine1Level2);
                levelHolder.AddChild(_portalOrangeLine2Level2);

                // Platform
                _platform3 = new Platform_Image(new Vec2(390, 850), 1, 0);
                _platform4 = new Platform_Image(new Vec2(960, 280), 0, 90);
                _platform5 = new Platform_Image(new Vec2(810, 280), 1, 90);
                _platform6 = new Platform_Image(new Vec2(660, 280), 0, 90);
                _platform7 = new Platform_Image(new Vec2(510, 280), 1, 90);

                levelHolder.AddChild(_platform3);
                levelHolder.AddChild(_platform4);
                levelHolder.AddChild(_platform5);
                levelHolder.AddChild(_platform6);
                levelHolder.AddChild(_platform7);

                // Blender
                _blenderLine2 = new BlenderLine(new Vec2(_blenderLevel2.x - 5, _blenderLevel2.y + 40), new Vec2(_blenderLevel2.x - 5, _blenderLevel2.y - 40));

                levelHolder.AddChild(_blenderLevel2);
                levelHolder.AddChild(_blenderLine2);

                AddLine(new Vec2(_blenderLevel2.x + 95, _blenderLevel2.y + 52), new Vec2(_blenderLevel2.x - 12, _blenderLevel2.y + 52));    // Right
                AddLine(new Vec2(_blenderLevel2.x - 12, _blenderLevel2.y - 52), new Vec2(_blenderLevel2.x + 95, _blenderLevel2.y - 52));    // Left 
                AddLine(new Vec2(_blenderLevel2.x - 12, _blenderLevel2.y + 52), new Vec2(_blenderLevel2.x - 58, _blenderLevel2.y + 35));    // Right oblique
                AddLine(new Vec2(_blenderLevel2.x - 58, _blenderLevel2.y - 35), new Vec2(_blenderLevel2.x - 12, _blenderLevel2.y - 52));    // Left oblique

                // Score balls
                _scoreball.Add(new ScoreBall(new Vec2(width / 2 + 310, height - 550)));
                _scoreball.Add(new ScoreBall(new Vec2(1100, 555)));
                _scoreball.Add(new ScoreBall(new Vec2(width / 2 - 300, height - 155)));

                // HUD
                levelHolder.AddChild(_HUD);

                // Pause menu
                _pauseMenu.visible = false;
                levelHolder.AddChild(_pauseMenu);
                break;


            case 5:
                _spawnBall = true;
                level3Active = true;

                ballSpawnPosition = new Vec2(100, height - 200);
                fruitImage = 1;

                // Border
                AddLine(new Vec2(width - width, height - height), new Vec2(width, height - height));    // Top
                AddLine(new Vec2(width, height), new Vec2(width - width, height));                      // Bottom
                AddLine(new Vec2(width - width, height), new Vec2(width - width, height - height));     // Left
                AddLine(new Vec2(width, height - height), new Vec2(width, height));                     // Right

                // Level              
                levelHolder.AddChild(_level3);

                // Lines
                AddLine(new Vec2(1300, 0), new Vec2(1440, 165));        // Top right diagonal    

                AddLine(new Vec2(1440, 820), new Vec2(730, 820));       // Right block line
                AddLine(new Vec2(730, 820), new Vec2(730, 900));        // Side line of right block   

                AddLine(new Vec2(478, 820), new Vec2(0, 820));          // Left block line 
                AddLine(new Vec2(478, 900), new Vec2(478, 820));        // Side line of left block 

                AddLine(new Vec2(0, 265), new Vec2(478, 265));          // Top left bottom line 
                AddLine(new Vec2(368, 155), new Vec2(0, 155));          // Top left top line 
                AddLine(new Vec2(368, 0), new Vec2(368, 155));          // Top left line 
                AddLine(new Vec2(478, 265), new Vec2(478, 0));          // Top left right line

                // Blue portal
                _portalBlueLineInLevel3 = new PortalLine(new Vec2(_portalBlueImageInLevel3.x - 100, _portalBlueImageInLevel3.y), new Vec2(_portalBlueImageInLevel3.x + 100, _portalBlueImageInLevel3.y));
                _portalBlueLineOutLevel3 = new PortalLine(new Vec2(_portalBlueImageOutLevel3.x + 100, _portalBlueImageOutLevel3.y), new Vec2(_portalBlueImageOutLevel3.x - 100, _portalBlueImageOutLevel3.y));

                levelHolder.AddChild(_portalBlueImageInLevel3);
                levelHolder.AddChild(_portalBlueImageOutLevel3);
                levelHolder.AddChild(_portalBlueLineInLevel3);
                levelHolder.AddChild(_portalBlueLineOutLevel3);

                // Orange portal
                _portalOrangeLine1Level3 = new PortalLine(new Vec2(_portalOrangeImageInLevel3.x + 100, _portalOrangeImageInLevel3.y), new Vec2(_portalOrangeImageInLevel3.x - 100, _portalOrangeImageInLevel3.y));
                _portalOrangeLine2Level3 = new PortalLine(new Vec2(_portalOrangeImageOutLevel3.x - 100, _portalOrangeImageOutLevel3.y), new Vec2(_portalOrangeImageOutLevel3.x + 100, _portalOrangeImageOutLevel3.y));

                levelHolder.AddChild(_portalOrangeImageInLevel3);
                levelHolder.AddChild(_portalOrangeImageOutLevel3);
                levelHolder.AddChild(_portalOrangeLine1Level3);
                levelHolder.AddChild(_portalOrangeLine2Level3);

                // Platform
                _platform8 = new Platform_Image(new Vec2(600, 250), 1, 0);
                _platform9 = new Platform_Image(new Vec2(740, 120), 0, 90);
                _platform10 = new Platform_Image(new Vec2(740, 355), 1, 90);
                _platform11 = new Platform_Image(new Vec2(740, 590), 0, 90);
                _platform12 = new Platform_Image(new Vec2(1320, 550), 0, 0);
                _platform13 = new Platform_Image(new Vec2(880, 550), 1, 0);

                levelHolder.AddChild(_platform8);
                levelHolder.AddChild(_platform9);
                levelHolder.AddChild(_platform10);
                levelHolder.AddChild(_platform11);
                levelHolder.AddChild(_platform12);
                levelHolder.AddChild(_platform13);

                // Blender
                _blenderLine3 = new BlenderLine(new Vec2(_blenderLevel3.x + 45, _blenderLevel3.y), new Vec2(_blenderLevel3.x - 45, _blenderLevel3.y));

                levelHolder.AddChild(_blenderLevel3);
                levelHolder.AddChild(_blenderLine3);

                AddLine(new Vec2(_blenderLevel3.x - 52, _blenderLevel3.y - 95), new Vec2(_blenderLevel3.x - 52, _blenderLevel3.y + 12));    // Left  
                AddLine(new Vec2(_blenderLevel3.x + 52, _blenderLevel3.y + 12), new Vec2(_blenderLevel3.x + 52, _blenderLevel3.y - 95));    // Right 
                AddLine(new Vec2(_blenderLevel3.x - 52, _blenderLevel3.y + 12), new Vec2(_blenderLevel3.x - 35, _blenderLevel3.y + 58));    // Left oblique
                AddLine(new Vec2(_blenderLevel3.x + 35, _blenderLevel3.y + 58), new Vec2(_blenderLevel3.x + 52, _blenderLevel3.y + 12));    // Right oblique

                // Rotatable triangle
                _rotatableTriangleImageLevel3 = new RotatableTriangle_Image(new Vec2(850, 130));
                levelHolder.AddChild(_rotatableTriangleImageLevel3);

                // Score balls
                _scoreball.Add(new ScoreBall(new Vec2(width / 2 + 410, height - 850)));
                _scoreball.Add(new ScoreBall(new Vec2(1325, 765)));
                _scoreball.Add(new ScoreBall(new Vec2(width / 2 + 605, height - 435)));

                // HUD
                levelHolder.AddChild(_HUD);

                // Pause menus
                _pauseMenu.visible = false;
                levelHolder.AddChild(_pauseMenu);
                break;


            case 6:
                _spawnBall = true;
                level4Active = true;

                ballSpawnPosition = new Vec2(1180, 800);
                fruitImage = 2;


                // Border
                AddLine(new Vec2(width - width, height - height), new Vec2(width, height - height));    // Top
                AddLine(new Vec2(width, height), new Vec2(width - width, height));                      // Bottom
                AddLine(new Vec2(width - width, height), new Vec2(width - width, height - height));     // Left
                AddLine(new Vec2(width, height - height), new Vec2(width, height));                     // Right

                // Level                
                levelHolder.AddChild(_level4);

                // Lines
                AddLine(new Vec2(785, 0), new Vec2(785, 350));          // Top left line
                AddLine(new Vec2(785, 350), new Vec2(0, 780));          // Diagonal left line
                AddLine(new Vec2(100, 900), new Vec2(785, 525));        // Diagonal bottom left line
                AddLine(new Vec2(785, 525), new Vec2(785, 755));        // Bottom left line
                AddLine(new Vec2(785, 755), new Vec2(625, 900));        // Bottom left line diagonal 

                AddLine(new Vec2(895, 160), new Vec2(1035, 0));         // Top right diagonal 
                AddLine(new Vec2(895, 760), new Vec2(895, 160));        // Right line 
                AddLine(new Vec2(1065, 900), new Vec2(895, 760));       // Bottom right diagonal

                AddLine(new Vec2(1440, 760), new Vec2(1280, 900));      // Bottom right corner 
                AddLine(new Vec2(1300, 0), new Vec2(1440, 165));        // Top right corner 

                AddLine(new Vec2(1190, 355), new Vec2(1040, 355));      // Square (L) top line
                AddLine(new Vec2(1040, 400), new Vec2(1190, 400));      // Square (L) bottom line
                AddLine(new Vec2(1040, 355), new Vec2(1040, 400));      // Square (L) left line
                AddLine(new Vec2(1190, 400), new Vec2(1190, 355));      // Square (L) right line

                AddLine(new Vec2(1390, 410), new Vec2(1230, 410));      // Square (R) top line
                AddLine(new Vec2(1230, 460), new Vec2(1390, 460));      // Square (R) bottom line
                AddLine(new Vec2(1230, 410), new Vec2(1230, 460));      // Square (R) left line
                AddLine(new Vec2(1390, 460), new Vec2(1390, 410));      // Square (R) right line

                // Blue portal
                _portalBlueLineInLevel4 = new PortalLine(new Vec2(_portalBlueImageInLevel4.x + 100, _portalBlueImageInLevel4.y), new Vec2(_portalBlueImageInLevel4.x - 100, _portalBlueImageInLevel4.y));
                _portalBlueLineOutLevel4 = new PortalLine(new Vec2(_portalBlueImageOutLevel4.x - 100, _portalBlueImageOutLevel4.y), new Vec2(_portalBlueImageOutLevel4.x + 100, _portalBlueImageOutLevel4.y));

                levelHolder.AddChild(_portalBlueImageInLevel4);
                levelHolder.AddChild(_portalBlueImageOutLevel4);
                levelHolder.AddChild(_portalBlueLineInLevel4);
                levelHolder.AddChild(_portalBlueLineOutLevel4);

                // Orange portal
                _portalOrangeLine1Level4 = new PortalLine(new Vec2(_portalOrangeImageInLevel4.x + 100, _portalOrangeImageInLevel4.y), new Vec2(_portalOrangeImageInLevel4.x - 100, _portalOrangeImageInLevel4.y));
                _portalOrangeLine2Level4 = new PortalLine(new Vec2(_portalOrangeImageOutLevel4.x - 100, _portalOrangeImageOutLevel4.y), new Vec2(_portalOrangeImageOutLevel4.x + 100, _portalOrangeImageOutLevel4.y));

                levelHolder.AddChild(_portalOrangeImageInLevel4);
                levelHolder.AddChild(_portalOrangeImageOutLevel4);
                levelHolder.AddChild(_portalOrangeLine1Level4);
                levelHolder.AddChild(_portalOrangeLine2Level4);

                // Platform
                _platform14 = new Platform_Image(new Vec2(600, 750), 0, 0);
                _platform15 = new Platform_Image(new Vec2(360, 450), 1, 60);
                _platform16 = new Platform_Image(new Vec2(240, 240), 0, 60);

                levelHolder.AddChild(_platform14);
                levelHolder.AddChild(_platform15);
                levelHolder.AddChild(_platform16);

                // Blender
                _blenderLine4 = new BlenderLine(new Vec2(_blenderLevel4.x + 40, _blenderLevel4.y + 35), new Vec2(_blenderLevel4.x, _blenderLevel4.y - 55));

                levelHolder.AddChild(_blenderLevel4);
                levelHolder.AddChild(_blenderLine4);

                AddLine(new Vec2(_blenderLevel4.x + 105, _blenderLevel4.y + 5), new Vec2(_blenderLevel4.x + 8, _blenderLevel4.y + 52));    // Left
                AddLine(new Vec2(_blenderLevel4.x - 35, _blenderLevel4.y - 40), new Vec2(_blenderLevel4.x + 52, _blenderLevel4.y - 90));    // Right 
                AddLine(new Vec2(_blenderLevel4.x + 8, _blenderLevel4.y + 52), new Vec2(_blenderLevel4.x - 38, _blenderLevel4.y + 58));    // Left oblique
                AddLine(new Vec2(_blenderLevel4.x - 70, _blenderLevel4.y - 5), new Vec2(_blenderLevel4.x - 35, _blenderLevel4.y - 40));    // Right oblique

                // Rotatable triangle
                _rotatableTriangleImageLevel4 = new RotatableTriangle_Image(new Vec2(660, 290), 90);
                levelHolder.AddChild(_rotatableTriangleImageLevel4);

                // Score balls
                _scoreball.Add(new ScoreBall(new Vec2(width / 2 + 415, height - 850)));
                _scoreball.Add(new ScoreBall(new Vec2(750, 290)));
                _scoreball.Add(new ScoreBall(new Vec2(width / 2 - 309, height - 400)));

                // HUD
                levelHolder.AddChild(_HUD);

                // Pause menu
                _pauseMenu.visible = false;
                levelHolder.AddChild(_pauseMenu);
                break;

            case 7:
                // Background
                levelHolder.AddChild(_backgroundInstruction1);

                // Instructions                
                levelHolder.AddChild(_instructions1);
                break;

            case 8:
                // Background
                levelHolder.AddChild(_backgroundInstruction2);

                // Instructions
                levelHolder.AddChild(_instructions2);
                break;

            case 9:
                // Background
                levelHolder.AddChild(_backgroundInstruction3);

                // Instructions
                levelHolder.AddChild(_instructions3);
                break;

            case 10:
                // Background
                levelHolder.AddChild(_backgroundInstruction4);

                // Instructions
                levelHolder.AddChild(_instructions4);
                break;

            case 11:
                menuIsActive = true;

                // Background
                _background = new Backgrounds(9);
                _quitButton = new Buttons(new Vec2(width / 2, height / 2 + 365), 2);
                _quitButton_Hover = new Buttons(new Vec2(width / 2, height / 2 + 365), 6);

                levelHolder.AddChild(_background);
                AddChild(_quitButton);
                AddChild(_quitButton_Hover);
                break;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      SpawnBall()                                                                          //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public void SpawnBall()
    {
        if (_spawnBall && numberOfBalls < 1 && !menuIsActive)
        {
            // Ball (moving object)
            Vec2 ballPos = ballSpawnPosition;
            Ball Playerball = new Ball(0, 0, 0, 27, ballPos, new Vec2(0, 0), true, false);
            _ball.Add(Playerball);

            Ball.acceleration = new Vec2(0, 0);

            _stepIndex = -1;

            foreach (Ball b in _ball)
            {
                AddChild(b);
            }

            // Apple image
            _appleimage = new Fruit_Image(ballPos, fruitImage);
            AddChild(_appleimage);
            _appleimage._ball = Playerball;

            // Arrow image
            _rotationArrow = new RotationArrow(ballPos);
            AddChild(_rotationArrow);
            _rotationArrow._ball = Playerball;

            _spawnBall = false;
            numberOfBalls++;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      HandleInput()                                                                        //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void HandleInput()
    {
        targetFps = Input.GetKey(Key.SPACE) ? 5 : 60;

        if (Input.GetKeyDown(Key.P))
        {
            _paused ^= true;
        }

        if (Input.GetKeyDown(Key.R))
        {
            LoadScene(_startSceneNumber);
            score = 0;
        }

        // NEEDS TO BE DISABLED LATER
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown(48 + i))
            {
                LoadScene(i);
            }
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      StepThroughMovers()                                                                  //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void StepThroughMovers()
    {
        if (_stepped)
        { // Move everything step-by-step: in one frame, only one mover moves
            _stepIndex++;

            if (_stepIndex >= _ball.Count)
            {
                _stepIndex = 0;
            }

            if (_ball[_stepIndex].moving)
            {
                _ball[_stepIndex].Step();
            }
        }

        else
        { // Move all movers every frame
            for (int i = 0; i < _ball.Count; i++)
            {
                if (_ball[i].moving)
                {
                    _ball[i].Step();
                }
            }
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      HandleAnimation()                                                                    //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void HandleAnimation()
    {
        if (startTransition)
        {
            _levelTransition = new LevelTransition();
            AddChildAt(_levelTransition, 1000);
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      HandleClickButton()                                                                  //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void HandleClickButton()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (menuIsActive)
            {

                if (_startButton.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    LoadScene(7);
                }

                if (_controlsButton.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    LoadScene(2);
                }

                if (_quitButton.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    LateDestroy();
                }
            }

            if (controlMenuIsActive)
            {
                if (_backButton != null)
                {
                    if (_backButton.HitTestPoint(Input.mouseX, Input.mouseY))
                    {
                        LoadScene(default);
                    }
                }
            }
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      HandleHoverButton()                                                                  //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void HandleHoverButton()
    {
        if (menuIsActive)
        {
            if (_startButton.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                _startButton.visible = false;
                _startButton_Hover.visible = true;
            }
            else
            {
                _startButton.visible = true;
                _startButton_Hover.visible = false;
            }

            if (_controlsButton.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                _controlsButton.visible = false;
                _controlsButton_Hover.visible = true;
            }
            else
            {
                _controlsButton.visible = true;
                _controlsButton_Hover.visible = false;
            }

            if (_quitButton.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                _quitButton.visible = false;
                _quitButton_Hover.visible = true;
            }
            else
            {
                _quitButton.visible = true;
                _quitButton_Hover.visible = false;
            }
        }

        if (controlMenuIsActive)
        {
            if (_backButton != null)
            {
                if (_backButton.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    _backButton.visible = false;
                    _backButton_Hover.visible = true;
                }
                else
                {
                    _backButton.visible = true;
                    _backButton_Hover.visible = false;
                }
            }
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      HandleHoverSound()                                                                   //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void HandleHoverSound()
    {
        if (menuIsActive)
        {
            if (_startButton.HitTestPoint(Input.mouseX, Input.mouseY) && startHoverSoundHasPlayed == false)
            {
                _hoverSound.Play();
                startHoverSoundHasPlayed = true;
            }
            else if (!_startButton.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                startHoverSoundHasPlayed = false;
            }

            if (_controlsButton.HitTestPoint(Input.mouseX, Input.mouseY) && controlsHoverSoundHasPlayed == false)
            {
                _hoverSound.Play();
                controlsHoverSoundHasPlayed = true;
            }
            else if (!_controlsButton.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                controlsHoverSoundHasPlayed = false;
            }

            if (_quitButton.HitTestPoint(Input.mouseX, Input.mouseY) && quitHoverSoundHasPlayed == false)
            {
                _hoverSound.Play();
                quitHoverSoundHasPlayed = true;
            }
            else if (!_quitButton.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                quitHoverSoundHasPlayed = false;
            }
        }

        if (controlMenuIsActive)
        {
            if (_backButton.HitTestPoint(Input.mouseX, Input.mouseY) && backHoverSoundHasPlayed == false)
            {
                _hoverSound.Play();
                backHoverSoundHasPlayed = true;
            }
            else if (!_backButton.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                backHoverSoundHasPlayed = false;
            }
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      startMusic()                                                                         //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void startMusic()
    {
        _backgroundMusicChannel = _backgroundMusic.Play();
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      stopMusic()                                                                          //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void stopMusic()
    {
        _backgroundMusicChannel.Stop();
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Update()                                                                             //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void Update()
    {
        HandleInput();
        HandleClickButton();
        HandleHoverButton();
        HandleHoverSound();
        HandleAnimation();
        SpawnBall();

        if (!_paused)
        {
            StepThroughMovers();

            if (_pauseMenu != null)
            {
                _backgroundMusicChannel.IsPaused = false;
                _pauseMenu.visible = false;
            }

        }

        if (_paused)
        {
            if (_pauseMenu != null)
            {
                _backgroundMusicChannel.IsPaused = true;
                _pauseMenu.visible = true;
            }
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      static void Main()                                                                   //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    static void Main()
    {
        new MyGame().Start();
    }
}
