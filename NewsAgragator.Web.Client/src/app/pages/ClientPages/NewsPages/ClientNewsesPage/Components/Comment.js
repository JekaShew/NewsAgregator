import React from 'react';
import { connect } from 'react-redux';

import '../../../ClientPages/ClientStyles.css';

const Comment = (props) => {

    const renderComponent = () => {
            return (
                <div className='comment'>
                    <div className='newsShortTitle'>{x.userName.value}</div>
                    <div className='newsShortDescription'>{x.text.value}</div> 
                </div>
            )
    }

    return (
            <div className="">
                {renderComponent()}
            </div>
    );
};

export default connect(mapStateToProps, mapDispatchToProps)(Comment);