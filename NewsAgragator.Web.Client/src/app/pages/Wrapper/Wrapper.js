import React, { Component } from 'react';

import { withRouter } from 'react-router';
/*import icons from '../../assets/icons';*/
import './Wrapper.css';
var ReactRouterDom = require('react-router-dom');
const NavLink = ReactRouterDom.NavLink;

class Wrapper extends Component {
    render() {
        return (
            <div className="wrapper">
                <div className="header">
                    <div className="title"><NavLink className="navLink" to="/Main">NewsAgregator</NavLink></div>
                    <div className='navItems'>
                        <div className="navItem"><NavLink className="navLink" to="/ClientWeather">Weather</NavLink></div>
                        <div className="navItem"><NavLink className="navLink" to="/ClientNewses">News</NavLink></div>

                        <div className="navItem"><NavLink className="navLink" to="/SignIn">Sign In</NavLink></div>
                    </div>
                </div>
                <div className="wrapper-content">
                    {this.props.children}
                </div>
                <div className="footer">
                    <div className="footerText1"> OOO "JSCompany" УНП 32947913740138 </div>
                    <div> 2024</div>
                </div>
            </div>
        );
    }
}

export default Wrapper;

//          <div className="navItem"><NavLink className="navLink" to="/weather">Weather</NavLink></div>
//          <div className="navItem"><NavLink className="navLink" to="/news"> News</NavLink></div>
//          <div className="navItem"><NavLink className="navLink" to="/authorization">Sign In</NavLink></div>