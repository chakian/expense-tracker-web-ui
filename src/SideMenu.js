// import React, { Component } from 'react';
// import { Nav, NavItem, Button } from 'react-bootstrap';
// import Sidebar from 'react-bootstrap-sidebar';
// import { MdMenu } from "react-icons/md";

// export default class SideMenu extends Component {

//     constructor(props) {
//         super(props);

//         this.state = {
//             isVisible: false,
//         };
//     }

//     updateModal(isVisible) {
//         this.setState({
//             isVisible: isVisible
//         });
//         this.forceUpdate();
//     }

//     render() {
//         return (
//             <div>
//                 <Button bsStyle="primary" onClick={() => this.updateModal(true)}><img src={MdMenu} alt="" /></Button>
//                 <Sidebar side='left' isVisible={this.state.isVisible} onHide={() => this.updateModal(false)}>
//                     <Nav>
//                         <NavItem href="#">Link 1</NavItem>
//                         <NavItem href="#">Link 2</NavItem>
//                         <NavItem href="#">Link 3</NavItem>
//                         <NavItem href="#">Link 4</NavItem>
//                     </Nav>
//                 </Sidebar>
//             </div>
//         );
//     }
// }
