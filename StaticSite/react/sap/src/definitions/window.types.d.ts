import { ComicHelper } from "../app";

// declare my helper in the window interface
declare global {
    interface Window {
        ComicHelper: ComicHelper
    }
  }