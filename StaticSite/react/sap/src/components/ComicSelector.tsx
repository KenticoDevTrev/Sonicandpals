// Simple Episode # entry and go
// includes Mode, current episode #
import React = require("react");
import { Button, FormGroup, FormLabel, Modal } from "react-bootstrap";
import { IComicSelectorProps } from "../interfaces/IComicSelectorProps";
import { IComicSelectorState } from "../interfaces/IComicSelectorState";
import DatePicker from 'react-date-picker';

export class ComicSelector extends React.Component<IComicSelectorProps, IComicSelectorState> {
    constructor(props) {
        super(props);

        this.state = {
            SelectedDate: (this.props.RefComic ? new Date(this.props.RefComic.date) : new Date(2004, 0, 1)),
            SelectedEpisode: (this.props.RefComic ? this.props.RefComic.episodeNumber.toString() : ""),
            SelectedChapter: (this.props.Chapters ? (this.props.RefComic ? this.props.RefComic.chapter : this.props.Chapters[0].title) : ""),
            Display: true
        }
    }

    componentDidMount() {
    }
    componentDidUpdate() {
    }

    handleClose = () : void => {
        this.setState({
            Display: false
        });
        this.props.CloseCallback();
    }

    dateChange = (value: Date): void => {
        this.setState({
            SelectedDate: value
        });
    }

    chapterChanged = (event: React.FormEvent<HTMLSelectElement>) : void => {
        var safeSearchTypeValue: string = event.currentTarget.value;
        this.setState({
            SelectedChapter: safeSearchTypeValue
        });
    }

    episodeChanged = (event: React.FormEvent<HTMLInputElement>) : void => {
        var safeSearchTypeValue: string = event.currentTarget.value;
        this.setState({
            SelectedEpisode: safeSearchTypeValue
        });
    }

    chapterSelected = () :void => {
        // Get current chapter
        const FoundChapter = this.props.Chapters?.find(x => {
            return x.title == this.state.SelectedChapter;
        });
        this.props.GoToEpisode(FoundChapter!.startEpisodeNumber);
        this.setState({
            Display: false
        });
        this.props.CloseCallback();
    }

    episodeSelected = () : void =>  {
        if(!isNaN(parseInt(this.state.SelectedEpisode))){
            this.props.GoToEpisode(parseInt(this.state.SelectedEpisode));
            this.setState({
                Display: false
            });
            this.props.CloseCallback();
        }
    }

    dateSelected = () : void => {
        this.props.GoToDate(this.state.SelectedDate);
        this.setState({
            Display: false
        });
        this.props.CloseCallback();
    }

    render() {
        const MinDate = new Date(2004, 0, 1);
        const MaxDate = new Date(2011, 5, 19);
        var ChapterList = (this.props.Chapters ? this.props.Chapters.map(function (chapter) {
            return <option value={chapter.title}>{chapter.title}</option>
        }, this) : null);
        return <Modal show={this.state.Display} onHide={this.handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Select A Comic</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <div className="ComicSelect">
                    {this.props.Chapters &&
                        <FormGroup className="form-group">
                            <FormLabel className="label">Select Chapter: </FormLabel>
                            <select className="form-control" value={this.state.SelectedChapter!} onChange={this.chapterChanged}>
                                {ChapterList}
                            </select>
                            <button className="ChapterSelect" onClick={this.chapterSelected}></button>
                        </FormGroup>
                    }
                    <FormGroup>
                        <FormLabel>Select Episode:</FormLabel>
                        <input className="form-control" type="number" value={this.state.SelectedEpisode} onChange={this.episodeChanged}></input>
                        <button className="EpisodeSelect" onClick={this.episodeSelected}></button>
                    </FormGroup>
                    <FormGroup>
                        <FormLabel>Select Date: </FormLabel>
                        <DatePicker className="form-control" minDate={MinDate} maxDate={MaxDate} onChange={this.dateChange} value={this.state.SelectedDate} ></DatePicker>
                        <button className="DateSelect" onClick={this.dateSelected}></button>
                    </FormGroup>
                </div>
            </Modal.Body >
            <Modal.Footer>
                <Button variant="secondary" onClick={this.handleClose}>Close</Button>
            </Modal.Footer>
        </Modal >
    }
}