﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace EarthSpace.Graphics
{
    /// <summary>
    /// An object than can be drawn.
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Registers the Drawable to call Draw() with the world's Draw loop.
        /// </summary>
        void Show();

        /// <summary>
        /// Draws this Drawable.
        /// </summary>
        /// <param name="spriteBatch"></param>
        void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Deregisters the Drawable to stop calling Draw() with the world's Draw loop.
        /// </summary>
        void Hide();

        //I love you.
        //Why not a top down?
        //
        //I was playing Thief Gold and I was impressed with the intricacy of the design
        //The beautiful thing was that a PERSON set the arrow trap with those pressure plates in between caskets
        //And I noticed and avoided it and took the treasure

        //All experiences possible in 2D mapping x to z (height) are equally as implamentable by converting x and y 

        // You have the same breadth for experience

        //It's not top down it's PCG sorry
        //I want to design a game. I've never actually created the content from start to finish it's always been random
        //I'm bored of that
        //And good PCG (a la Spelunky) takes sooooo much expertise
        //So I wanna design something


        //I want top down though, that's my only request, movement like Pickn'Sticks. Open world.
        //ok we could technically design an entire world
        //but why

        //You can have a linear experience.
        //MMOS do this.
        //It's called, phishing. The world changes as you progress through the story. Therefore its linear. 
        //For example, we have the age mechanic. That is an underlying linear force, but on top of that we can add story and major events.
        //As time progresses sure things age, but we can also have world changing occurences. An astroid hits, the town is destroyed.
        
        //I really liked your idea of aging and dynamic world. Your affect on the game, but the experience is built by us not PCG

        //kkkLAN party

        //Can we leave this dialogue in the code.
        //it doesnt affect the game play, andi ts our story. Our dialogues are precious treasure
        //that can be copy pasted into a Word Doc and saved outside of a public repo.

        //nah, but it would be so chill to have this in the repo. Fuck it, who cares. We'll probably privatise the repos when we have to apply for real jobs

        //IDrawable.cs 500 lines 
        //We are the best coders dude.

        //TIME TO COMMIT.

        //I want top-down stealth puzzles
        
        //I don't. Not this game. When we go 3d. nächtes Jahre

        //Still multiple characters thing
        //same time frame
        //SO COOL

        //Potions that slow down time. (Wild herbs)
        //(this is an easter egg)


        //We need an item system. They should be physical and dropped from entities when killed (minecraft style drops)


        //This is how I get internships
        //I(freqntr)

        //you don't even want it. 
        //Lol I lead girls on just like employers. in style bitch
        // lauara anderson

        //I send all the mixed messages

        //like today I almost ignored her (most days)
        //the anonomly was when i talked to her (after we danced) that's the only fukkinh time

        //On the non-development side as well
        // This means blazing. jk

        //Find the slowest moving, oldest, most inefficient industry, and develop GREAT software for it. That's how you make real money.

        //Do you love us robert?
        //We love you
        //join us. I heard this is like multipalyer or something.

        //I don't have an internet connection
        //NO MULTIPLAY

        //fUCK, gg
        //This is the best fucking soruce code file I've ever partaken in.

        //I'm deleting this when you leave.
        //Please don't :(

        //let's privatize the repo immediately

        //Sure

        //Gonna lead them on, and stop talking to them

        //Like the last 3 internships.

        //This is about you and Xmission isn't it
        //:(

        //not at Google
        //Google asks for SAT scores and GPA even if you're like 45 applying for a job.

        //LOL. FUCK GOOGLE. THEY HIRE DIRECTLY FROM MIT
        //LOL FUCK

        //She aint gonna look at yo resume beetch? <LA

        //who? what? ^^? who is she? im so confused. // did i right dat? wtf

        //XNA LOOKS SO NICE
        //FUCK OPENGL
        //DIRECTX 4 LUFE

        //waste of my time ,-- whut being a live? a life a liff A LIVE ALIVEwhut? this whole talk has been

    }
}
