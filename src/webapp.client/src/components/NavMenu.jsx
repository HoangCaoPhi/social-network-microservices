import React, { Component } from "react";
import { Link } from "react-router-dom";
import {
  Collapse,
  Container,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Button,
} from "reactstrap";
import "./NavMenu.css";

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor(props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true,
      loggedIn: false,
      logoutUrl: "/bff/logout",
      idToken: "", // State for idToken
      accessToken: "", // State for accessToken
      modal: false, // State for modal
      tokenFetching: false, // State for API call management
      copySuccess: false, // State to manage copy success message
    };

    this.fetchIsUserLoggedIn = this.fetchIsUserLoggedIn.bind(this);
    this.toggleModal = this.toggleModal.bind(this);
    this.fetchAccessToken = this.fetchAccessToken.bind(this); // Bind new method
    this.copyToClipboard = this.copyToClipboard.bind(this); // Bind copy method
  }

  componentDidMount() {
    (async () => this.fetchIsUserLoggedIn())();
  }

  toggleNavbar() {
    this.setState({
      collapsed: !this.state.collapsed,
    });
  }

  async fetchIsUserLoggedIn() {
    try {
      const response = await fetch("/bff/user", {
        headers: {
          "X-CSRF": 1,
        },
      });

      if (response.ok && response.status === 200) {
        const data = await response.json();
        const logoutUrl =
          data.find((claim) => claim.type === "bff:logout_url")?.value ?? this.state.logoutUrl;

        // Store id_token for later use
        const idToken = data.find((claim) => claim.type === "id_token")?.value ?? "";
        this.setState({ loggedIn: true, logoutUrl, idToken }); // Update state
      }
    } catch (e) {
      console.error(e);
      this.setState({ loggedIn: false });
    }
  }

  toggleModal() {
    this.setState({ modal: !this.state.modal, copySuccess: false }); // Reset copy success state when toggling modal
  }

  async fetchAccessToken() {
    this.setState({ tokenFetching: true });  
    try {
      const response = await fetch("/api/auth/access-token", {
        headers: {
          "X-CSRF": 1,
        },
      });

      if (response.ok && response.status === 200) {
        const data = await response.json();
        this.setState({ accessToken: data?.accessToken });  
      } else {
        console.error("Failed to fetch access token");
        this.setState({ accessToken: "Unable to fetch access token." });
      }
    } catch (error) {
      console.error("Error fetching access token:", error);
      this.setState({ accessToken: "Error fetching access token." });
    } finally {
      this.setState({ tokenFetching: false });  
      this.toggleModal();  
    }
  }

  // Method to copy access token to clipboard
  async copyToClipboard() {
    try {
      await navigator.clipboard.writeText(this.state.accessToken);
      this.setState({ copySuccess: true }); // Update state to show success
    } catch (err) {
      console.error('Failed to copy: ', err);
      this.setState({ copySuccess: false }); // Optionally handle error
    }
  }

  render() {
    return (
      <header>
        <Navbar
          className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3"
          light
        >
          <Container>
            <NavbarBrand tag={Link} to="/">
              React Bff Sample
            </NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse
              className="d-sm-inline-flex flex-sm-row-reverse"
              isOpen={!this.state.collapsed}
              navbar
            >
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">
                    Home
                  </NavLink>
                </NavItem>
                {this.state.loggedIn && (
                  <>
                    <NavItem>
                      <NavLink
                        tag={Link}
                        className="text-dark"
                        to="/user-session"
                      >
                        Show User Session
                      </NavLink>
                    </NavItem>
                    <NavItem>
                      <NavLink
                        className="text-dark"
                        onClick={this.fetchAccessToken} // Call API to fetch access token
                        href="#"
                      >
                        Show Access Token
                      </NavLink>
                    </NavItem>
                  </>
                )}
                <NavItem>
                  <a
                    className="text-dark nav-link"
                    href={
                      this.state.loggedIn 
                      ? `${this.state.logoutUrl}` 
                      : "/bff/login"
                    }
                  >
                    {this.state.loggedIn ? "Logout" : "Login"}
                  </a>
                </NavItem>
              </ul>
            </Collapse>
          </Container>
        </Navbar>

        {/* Modal to display Access Token */}
        <Modal isOpen={this.state.modal} toggle={this.toggleModal}>
          <ModalHeader toggle={this.toggleModal}>Access Token</ModalHeader>
          <ModalBody className="modal-body">
            {this.state.tokenFetching 
              ? "Fetching access token..." 
              : this.state.accessToken || "No Access Token Available"}
          </ModalBody>
          <ModalFooter>
            <Button color="secondary" onClick={this.toggleModal}>Close</Button>
            <Button color="primary" onClick={this.copyToClipboard}>Copy Access Token</Button> {/* Copy button */}
          </ModalFooter>
          {this.state.copySuccess && <div className="text-success">Access token copied to clipboard!</div>} {/* Success message */}
        </Modal>
      </header>
    );
  }
}
