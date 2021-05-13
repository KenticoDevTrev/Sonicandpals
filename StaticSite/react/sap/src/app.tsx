import * as ReactDOM from 'react-dom';
import * as React from 'react';
import {ComicZone} from './components/ComicZone'
import { ComicMode } from './enums/ComicMode';

// Determine mode by the page
const CurrentMode = ComicMode.Daily;
const IsHomePage = true;

ReactDOM.render(
    <ComicZone IsHomepage={IsHomePage} Mode={CurrentMode} />
, document.getElementById("ComicZone"));
