import React from 'react';
import { connect } from 'react-redux';

import '../../../ClientStyles.css';

const Comment = (props) => {

    const renderComponent = () => {
            return (
                <div className='comment'>
                    <div className='newsShortTitle'>{props.data.userName}</div>
                    <div className='newsShortDescription'>{props.data.text}</div> 
                </div>
            )
    }

    return (
            <div className="">
                {renderComponent()}
            </div>
    );
};

export default Comment;