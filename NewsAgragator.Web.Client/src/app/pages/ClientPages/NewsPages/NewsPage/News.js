import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { loadData, remove, aggregateNews } from './actions';
import '../../../AdminPages/EditPage.css';

const News = (props) => {

    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();


    // change to Load more with skip take
    const btnAggregateClick = () => {
        props.aggregate();
    }

    const btnEditSelectedClick = (id) => {
        navigate('/NewsDetails/' + id);
    }


    const renderNews = () => {
        console.log("RenderTable");
        console.log(props);
        console.log(props.value);

        if (loading == false) {
            return (
                <div>
                    {props.value.news.value.map(x => (
                        // create css
                            <div className="ClientNewsItem" key={x.id.value}>
                                <div>{x.title.value != null ? x.title.value : "No data"}</div>       
                                <div>{x.date.value != null ? x.date.value : "No data"}</div>
                                <div>{x.positiveRating.value != null ? x.positiveRating.value : "No data"}</div>
                                <button className="btnAddChange" onClick={() => btnEditSelectedClick(x.id.value)}>Details</button> 
                            </div>
                        ))}

                </div> 
            )
        }
        else
            return
        (<div className="items loading">
            <FontAwesomeIcon icon={faSpinner} />
        </div>)
    }

    const beforeRender = () => {
        console.log("BeforeRender");
        setLoading(true);
        props.loadData();
    }

    useEffect(() => {
        setLoading(props.value.news.loading);
    }, [props.value.news.loading]);

    useEffect(() => {
        beforeRender();
    }, []);


    return (
        <Wrapper>
            <div className="editPage">
                <div className="pageTitle"> News </div>
                <div className="btnsAboveTable">
                    <button className="btnAddChange" onClick={() => btnAggregateClick()}>Load More</button>
                </div>
                {renderNews()}
            </div>        
        </Wrapper>
    );
};

const mapDispatchToProps = dispatch => {
    return {

        loadData: () => dispatch(loadData()),
        remove: (id) => dispatch(remove(id)),
        aggregate:() => dispatch(aggregateNews()),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.news,
});

export default connect(mapStateToProps, mapDispatchToProps)(News);