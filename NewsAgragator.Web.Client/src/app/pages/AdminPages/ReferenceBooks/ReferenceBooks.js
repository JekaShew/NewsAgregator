import React from 'react';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../Wrapper/Wrapper';


const ReferenceBooks = () => {
    const navigate = useNavigate();

    const goToPage = (pageTitle) => {
        navigate(`/${pageTitle}`);
    }


    return (

        <Wrapper>
            <div className="referenceBooks">
                <div className="referenceBooksTitle"></div>
                <div>
                    <div className="referenceBooksContainer">
                        <div className="referenceBooksTitle">Accounts</div>
                        <button className="btnGoToPage" onClick={() => goToPage("AccountStatuses")}>AccountStatuses</button>
                        <button className="btnGoToPage" onClick={() => goToPage("Accounts")}>Accounts</button>

                        <button className="btnGoToPage" onClick={() => goToPage("Roles")}>Roles</button>
                        <button className="btnGoToPage" onClick={() => goToPage("Policies")}>Policies</button>
                    </div>
                    <div className="referenceBooksContainer">
                        <div className="referenceBooksTitle">Complaints</div>
                        <button className="btnGoToPage" onClick={() => goToPage("Complaints")}>Complaints</button>
                        <button className="btnGoToPage" onClick={() => goToPage("ComplaintTypes")}>Compalint Types</button>
                        <button className="btnGoToPage" onClick={() => goToPage("ComplaintStatuses")}>Compalint Statuses</button>

                        <button className="btnGoToPage" onClick={() => goToPage("NotificationMessages")}>Notification Messages</button>
                    </div>
                    <div className="referenceBooksContainer">
                        <div className="referenceBooksTitle">News</div>
                        <button className="btnGoToPage" onClick={() => goToPage("News")}>News</button>
                        <button className="btnGoToPage" onClick={() => goToPage("NewsStatuses")}>News Statuses</button>

                        <button className="btnGoToPage" onClick={() => goToPage("Comments")}>Comments</button>
                    </div>
                    <div className="referenceBooksContainer">
                        <div className="referenceBooksTitle">Weather</div>
                        <button className="btnGoToPage" onClick={() => goToPage("Weathers")}>Weather</button>
                        <button className="btnGoToPage" onClick={() => goToPage("WeatherStatuses")}>Weather Statuses</button>
                    </div>

                </div>

            </div>
        </Wrapper>
    );
};



export default ReferenceBooks;