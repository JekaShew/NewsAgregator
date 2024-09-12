import React from 'react';
import './ModalConfirmation.css';


const ModalConfirmation = (props) => {

    console.log("ModalConfirmation");
    console.log(props);

    return (
        <div className=""  >
            <div className="modal-dialog" >
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title" >Deleting</h5>
                        <button onClick={() => props.onClose()} type="button" className="close"  >
                            <span >&times;</span>
                        </button>
                    </div>
                    <div className="modal-body">
                        Are You sure You want to delete {props.title} ?
                    </div>
                    <div className="modal-footer">
                        <button onClick={() => props.onDelete(props.id)} type="button" className="btn btn-danger">Delete</button>
                        <button onClick={() => props.onClose()} type="button" className="btn btn-secondary" >Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    );
};


export default ModalConfirmation;