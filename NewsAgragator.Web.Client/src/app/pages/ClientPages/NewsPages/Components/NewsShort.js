import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import '../../../ClientPages/ClientStyles.css';

const NewsShortComponent = (props) => {

    const navigate = useNavigate();

    const btnReadMore = (id) => {
        navigate('/ClientNewsFull/' + id);
    }

    const renderComponent = () => {
            return (
                <div className='newsShort'>
                    <div className='newsShortTitle'>{props.data.title.value}</div>
                    <div className='newsShortDescription'>{props.data.description.value}</div>
                    
                    <div className='btnReadMore' onClick={() => btnReadMore(props.data.id.value)}> Read More </div>
                </div>
            )
    }

    return (
            <div className="">
                {renderComponent()}
            </div>
    );
};


export default NewsShortComponent;