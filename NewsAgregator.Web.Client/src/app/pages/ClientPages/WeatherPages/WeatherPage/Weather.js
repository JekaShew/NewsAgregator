import React from 'react';
import { connect } from 'react-redux';
import Wrapper from '../../../Wrapper/Wrapper';
import './Weather.css';
import InputObject from '../../../customComponents/InputObject';
/*import { select } from './actions';*/

var ReactRouterDom = require('react-router-dom');

const NavLink = ReactRouterDom.NavLink;
const withRouter = ReactRouterDom.withRouter;


class Weather extends React.Component {
    constructor(props) {
        super(props);
/*        this.props.loadParameters();*/
    }

 

    render() {
        return (
            <Wrapper>
                <div className="weather">

                    <div>      
                        <div>
                            <div className="">
                                <div className="">City</div>
                                
                            </div>
                        </div>
                    </div>
                    <div className="">

                    </div>
                </div>
            </Wrapper>
        )
    };
}

const mapDispatchToProps = dispatch => {
    return {
        //loadParameters: () => dispatch(loadParameters()),
        //select: (name, value) => dispatch(select(name, value)),
    }
}

function mapStateToProps(state) {
    return {

        value: state.weather,
    };
}
export default connect(mapStateToProps, mapDispatchToProps)(withRouter(Weather));


//<InputObject
//    value={ }
//    options={ }
//    placeholder="City"
//    onClick={(val) => this.props.select('City', val)}
///>