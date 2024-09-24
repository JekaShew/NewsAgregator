import initialState from "../../../../../initialState";

export default (state = initialState.clientNews, action) => {
    switch (action.type) {

        case "CLIENTNEWS_LOADALL_SUCCESS":
            return {
                ...state,
                    loading: false,
                    newses: action.data.map(tpr => ({
                        ...Object.fromEntries(Object.entries(tpr).map(x => ([
                            x[0],
                            {
                                value: x[1],
                            },
                        ]))), 
                    })),
            }

        case "CLIENTNEWS_LOADALL_START":
            return {
                ...state,
                    loading: true,   
            }

        default:
            return state
    }
}