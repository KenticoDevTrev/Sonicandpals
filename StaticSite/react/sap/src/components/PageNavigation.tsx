import React = require("react");
import { Button, Modal, Nav } from "react-bootstrap";
import { IPageNavigationProps } from "../interfaces/IPageNavigationProps";
import { IPageNavigationState } from "../interfaces/IPageNavigationState";
import { AjaxHelper } from "../libraries/AjaxHelper";
import { GetPageRequest } from "../models/GetPageRequest";
import { GetPageResponse } from "../models/GetPageResponse";
import { NavigationItem } from "../models/NavigationItem";

export class PageNavigation extends React.Component<IPageNavigationProps, IPageNavigationState> {
    ajaxHelper: AjaxHelper

    constructor(props) {
        super(props);
        this.ajaxHelper = new AjaxHelper();
        this.state = {
            Pages: new Array<NavigationItem>(),
            DisplayPage: false,
            DisplayedPage: null
        }
        this.handleClose = this.handleClose.bind(this);

        this.loadPages();
    }
    componentDidMount() {
    }
    componentDidUpdate() {
    }

    loadPages(): void {
        this.ajaxHelper.postRequest<Array<NavigationItem>>("//api.sonicandpals.com/api/GetNavigation").then(pages => {
            this.setState({
                Pages: pages
            });
        });
    }

    displayPage(pageIdentifier: string): void {
        const PageRequest: GetPageRequest = {
            PageIdentifier: pageIdentifier
        };
        this.ajaxHelper.postRequest<GetPageResponse>("//api.sonicandpals.com/api/GetPage", PageRequest).then(response => {
            if (response.error) {
                alert(response.error);
            } else {
                this.setState({
                    DisplayPage: true,
                    DisplayedPage: response.page
                });
            }
        });
    }
    handleClose(): void {
        this.setState({
            DisplayPage: false
        });
    }

    render() {
        let Loading =
        <div className="text-center">
            <div className="spinner-border text-primary" role="status">
                <span className="sr-only">Loading...</span>
            </div>
        </div>;

        if (this.state.Pages.length == 0) {
            return Loading;
        } else {
            var PageList = this.state.Pages.map(function (page) {
                //@ts-ignore
                if (page.uRL) {
                    return <Nav.Item key={page.pageIdentifier} >
                        <Nav.Link href={page.uRL} >{page.title}</Nav.Link>
                    </Nav.Item>;
                } else {
                    return <Nav.Item key={page.pageIdentifier} >
                        <Nav.Link eventKey={page.pageIdentifier}>{page.title}</Nav.Link>
                    </Nav.Item>;
                }
            }, this);

            return <React.Fragment>
                <Nav className="justify-content-center" onSelect={(selectedKey) => this.displayPage(selectedKey as string)} >
                    {PageList}
                </Nav>
                {this.state.DisplayPage &&
                    <Modal show={this.state.DisplayPage} onHide={this.handleClose}>
                        <Modal.Header closeButton>
                            <Modal.Title>{this.state.DisplayedPage?.title}</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                        <div dangerouslySetInnerHTML={{
                                __html: this.state.DisplayedPage?.htmlContent!
                            }}></div>
                            </Modal.Body>
                        <Modal.Footer>
                            <Button variant="secondary" onClick={this.handleClose}>
                                Close
                            </Button>
                        </Modal.Footer>
                    </Modal>
                }
            </React.Fragment>
        }

    }
}
