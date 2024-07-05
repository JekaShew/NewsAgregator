import React from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { select, add, save, loadParameters, loadData, clearState } from './actions';
import InputObject from '../../../../customComponents/InputObject/InputObject';
import '../../../AdminPages/EditPage.css';


const EditNews = (props) => {

    const navigate = useNavigate();

    const addORchangeBtn = (addORchange) => {
        if (addORchange == "Add")
            return (<button className="btnAddChange" onClick={() => addNews}>Add</button>);
        else if (addORchange == "Change")
            return (<button className="btnAddChange" onClick={() => changeNews}>Change</button>);

    }

    const changeNews = () => {
        const data = Object.fromEntries(Object.entries(props.value.editNews).map(([key, value]) => [key, value.value]));

        const formData = new FormData();

        for (const key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        props.change(formData);
        navigate("/News");
    }

    const addNews = () => {
        const data = Object.fromEntries(Object.entries(props.value.editNews).map(([key, value]) => [key, value.value]));

        const formData = new FormData();

        for (const key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        formData.delete('id');
        formData.append('id', null);

        props.add(formData);
        navigate("/News");
    };

    const beforeRender = () => {
        if (props.match.params.id) {
            props.loadData(props.match.params.id);
            addORchangeBtn("Change");
        }
        else {
            props.clearState();
            props.loadParameters();
            addORchangeBtn("Add");
        }
    }

    useEffect(() => {
        beforeRender();
    }, []);


    console.log(props);
    return (

        <Wrapper>
            <div className="editPage">
                <div className="pageTitle">Edit News </div>
                <div className="editPageInputs">
                    <div className="divInput">
                        <div className="inputTitle">Title</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Title"
                            value={props.value.editNews.title.value}
                            onChange={(e) => props.select("title", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Text</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Text"
                            value={props.value.editNews.text.value}
                            onChange={(e) => props.select("text", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Date</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Date"
                            value={props.value.editNews.date.value}
                            onChange={(e) => props.select("date", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Positive Rating</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Positive Rating"
                            value={props.value.editNews.positiveRating.value}
                            onChange={(e) => props.select("positiveRating", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">News Status</div>
                        <InputObject
                            id="newsStatus"
                            value={props.value.editNews.newsStatus.value}
                            options={props.value.editNews.newsStatus.options}
                            placeholder="NewsStatus"
                            onClick={(val) => this.props.select('newsStatus', val)}
                        />
                    </div>
                </div>
                <button className="btnAddChange" onClick={() => addNews}>Add</button>
                {
                    addORchangeBtn(props.addORchange)
                }
            </div>
        </Wrapper>
    );
};

const mapDispatchToProps = dispatch => {
    return {
        select: (name, value) => dispatch(select(name, value)),
        add: (newNews) => dispatch(add(newNews)),
        save: (updatedNews) => dispatch(save(updatedNews)),
        loadParameters: () => dispatch(loadParameters()),
        loadData: (id) => dispatch(loadData(id)),
        clearState: (data) => dispatch(clearState(data)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.editNews,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditNews);