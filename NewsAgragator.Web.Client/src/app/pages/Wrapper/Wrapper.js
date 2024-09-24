import React, { Component } from 'react';
import { connect } from 'react-redux';
import { signOut } from '../ClientPages/AccountPages/Authorization/actions';
import { withRouter } from 'react-router';
// import { useNavigate } from 'react-router-dom';
/*import icons from '../../assets/icons';*/
import './Wrapper.css';
var ReactRouterDom = require('react-router-dom');
const NavLink = ReactRouterDom.NavLink;




class Wrapper extends Component {

    render() {
        console.log(JSON.parse(localStorage.getItem("AUTHORIZATION")) == null);
        return (
            <div className="wrapper">
                <div className="header">
                    <div className="title"><NavLink className="navLink" to="/Main">NewsAgregator</NavLink></div>
                    <div className='navItems'>
                        <div className="navItem"><NavLink className="navLink" to="/ClientWeather">Weather</NavLink></div>
                        <div className="navItem"><NavLink className="navLink" to="/ClientNewses">News</NavLink></div>
                        {
                            
                            JSON.parse(localStorage.getItem("AUTHORIZATION")) != null ? 
                                <div className="navItem">
                                    <NavLink className="navLink" to="/ClientEditAccount">
                                        {JSON.parse(localStorage.getItem("AUTHORIZATION")).userName} 
                                    </NavLink>
                                    <div className='navLink' onClick={() => this.props.signOut()}>
                                        <NavLink className="navLink" to="/Main"> 
                                            Sign OUT
                                        </NavLink>
                                    </div>
                                </div> 
                                :
                                <div className="navItem"><NavLink className="navLink" to="/SignIn">Sign In</NavLink></div>
                        }
                        
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

const mapDispatchToProps = {
    signOut,
  };
  
  export default connect(null, mapDispatchToProps)(Wrapper);

// export default Wrapper;

//          <div className="navItem"><NavLink className="navLink" to="/weather">Weather</NavLink></div>
//          <div className="navItem"><NavLink className="navLink" to="/news"> News</NavLink></div>
//          <div className="navItem"><NavLink className="navLink" to="/authorization">Sign In</NavLink></div>