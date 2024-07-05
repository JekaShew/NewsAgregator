import React from 'react';

import './InputObject.css';
var ReactRouterDom = require('react-router-dom');


const withRouter = ReactRouterDom.withRouter;


class InputObject extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            show: false,
            query: "",

        };
        this.textInput = React.createRef();
        this.toggle = this.toggle.bind(this);
        this.getText = this.getText.bind(this);
        this.getOptions = this.getOptions.bind(this);
        this.editQuery = this.editQuery.bind(this);
        this.getConstantOptions = this.getConstantOptions.bind(this);


    }

    toggle() {

        this.setState({

            show: !this.state.show,

        });

        this.textInput.current.focus();
    }

    editQuery(query) {
        this.setState({
            query: query,
        })
    }


    getText() {

        if (!this.props.value) {
            return this.props.placeholder;
        }
        let val = this.props.value && this.props.options && this.props.options.filter(o => o.id == this.props.value);
        if (!val || !val.length || !val[0]) {
            val = this.props.placeholder;
        }
        return val[0].text;
    }

    select(id) {
        this.props.onClick(id);
        this.setState({
            show: false,
        });

    }

    getOptions() {
        return (
            this.props.options && this.props.options.filter(o => o.text.toLowerCase().includes(this.state.query.toLowerCase())).map(x => (
                <div key={x.id} onClick={() => this.select(x.id)} >{x.text}</div>
            ))
        );
    }

    getConstantOptions() {
        return [{ id: '', text: 'Any' }].map(x => (
            <div key={x.id} onClick={() => this.select(x.id)} >{x.text}</div>
        ));
    }

    render() {
        return (

            <div className="">
                <div className="" onClick={this.toggle} >
                    <span style={{ display: (this.state.show ? 'none' : 'block') }}>{this.getText()}</span>
                    <input
                        style={{ display: (this.state.show ? 'block' : 'none') }}
                        onClick={e => e.stopPropagation()}
                        ref={this.textInput}
                        className=""
                        type="text"
                        onChange={(e) => this.editQuery(e.target.value)}
                    />
                    <div className="" style={{ display: this.state.show ? 'block' : 'none' }}>
                        <div className="">
                            {this.getConstantOptions()}
                            {this.getOptions()}
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}
export default InputObject;