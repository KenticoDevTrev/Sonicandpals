// Simple Episode # entry and go
// includes Mode, current episode #
import React = require("react");
import { Button, Modal } from "react-bootstrap";
import { IComicShareProps } from "../interfaces/IComicShareProps";
import { IComicShareState } from "../interfaces/IComicShareState";
import { EmailIcon, EmailShareButton, FacebookIcon, FacebookShareButton, PinterestIcon, PinterestShareButton, TwitterIcon, TwitterShareButton } from "react-share";

export class ComicShare extends React.Component<IComicShareProps, IComicShareState> {
    constructor(props) {
        super(props);

        this.state = {
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

    copyToClipboard = (url : string) => {
        const el = document.createElement('textarea');
        el.value = url;
        document.body.appendChild(el);
        el.select();
        document.execCommand('copy');
        document.body.removeChild(el);
      }

    render() {
        const ShareUrl = "https://www.sonicandpals.com/index.html?Episode="+this.props.RefComic.episodeNumber.toString();
        const ShareTitle = "Sonicandpals #"+this.props.RefComic.episodeNumber.toString()+" - "+this.props.RefComic.title;
        const ShareHashTag = "Sonicandpals";
        const ShareHashTagArray = new Array<string>();
        ShareHashTagArray.push(ShareHashTag);
        let FacebookShare = //@ts-ignore: https://github.com/nygardk/react-share/issues/277 */}
        <FacebookShareButton hashtag={ShareHashTag} quote={ShareTitle} url={ShareUrl}>
        <FacebookIcon iconFillColor="white" round={true} />
        </FacebookShareButton>
        let TwitterShare = //@ts-ignore: https://github.com/nygardk/react-share/issues/277 */}
        <TwitterShareButton hashtags={ShareHashTagArray} title={ShareTitle} url={ShareUrl}>
        <TwitterIcon iconFillColor="white" round={true} />
        </TwitterShareButton>
        let PinterestShare = //@ts-ignore: https://github.com/nygardk/react-share/issues/277 */}
        <PinterestShareButton media={"https://www.sonicandpals.com/"+this.props.RefComic.imageUrl.replace("~", "")} description={ShareTitle} url={ShareUrl}>
        <PinterestIcon iconFillColor="white" round={true} />
        </PinterestShareButton>
        let EmailShare = //@ts-ignore: https://github.com/nygardk/react-share/issues/277 */}
        <EmailShareButton subject={ShareTitle} url={ShareUrl}>
        <EmailIcon iconFillColor="white" round={true} />
        </EmailShareButton>
        

        return <Modal show={this.state.Display} onHide={this.handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Share this Episode</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <div>
                {FacebookShare} {TwitterShare} {PinterestShare} {EmailShare}
                </div>
                <div className="d-flex flex-row mt-3">
                <button className="btn btn-primary" onClick={() => this.copyToClipboard(ShareUrl)} style={{width: "60px"}}>Copy</button> <input type="text" id="txtShareUrl" className="form-control" value={ShareUrl}/> 
                </div>
            </Modal.Body >
            <Modal.Footer>
                <Button variant="secondary" onClick={this.handleClose}>Close</Button>
            </Modal.Footer>
        </Modal >
    }
}