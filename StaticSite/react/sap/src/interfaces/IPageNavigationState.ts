import { NavigationItem } from "../models/NavigationItem";
import { Page } from "../models/Page";

export interface IPageNavigationState {
    Pages: Array<NavigationItem>
    DisplayPage: boolean;
    DisplayedPage: Page | null;
}