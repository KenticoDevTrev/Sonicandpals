import * as ReactDOM from 'react-dom';
import * as React from 'react';
import {ComicZone} from './components/ComicZone'
import { ComicMode } from './enums/ComicMode';

// Determine mode by the page
const CurrentMode = ComicMode.Daily;
const IsHomePage = true;

export class ComicHelper {

    static HelloWorld() : void {
        alert("Hello World");
    }
    static RenderComicByDate(EpisodeDate : Date) : void {
        const CurrentMode = ComicMode.Daily;
        ReactDOM.render(
            <ComicZone IsHomepage={false} Mode={CurrentMode} Date={EpisodeDate} />
        , document.getElementById("ComicZone"));
    }
    
    static RenderHomePage() : void {
    
        ReactDOM.render(
            <ComicZone IsHomepage={true} Mode={ComicMode.Daily} />
        , document.getElementById("ComicZone"));
    }
    static RenderComicByEpisode(EpisodeNumber : number) : void {
        ReactDOM.render(
            <ComicZone IsHomepage={false} Mode={ComicMode.Episode} EpisodeNumber={EpisodeNumber} />
        , document.getElementById("ComicZone"));
    }
}

// Sets the helper to the window
window.ComicHelper = ComicHelper;
