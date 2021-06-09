export interface IComicRatingProps {
    AverageRating : number;
    EpisodeNumber: number;
    EpisodeSubNumber: number | null;
    ErrorCallback(error : string) : void;
}