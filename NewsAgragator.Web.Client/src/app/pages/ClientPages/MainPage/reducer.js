import initialState from "../../../../initialState";

export default (state = initialState.clientMain, action) => {
    switch (action.type) {

        case "MAIN_LOADALL_SUCCESS":
            return {
                ...state,
                loading: false,

                topNewses: action.data.map(tpr => ({
                    ...Object.fromEntries(Object.entries(tpr).map(x => ([
                        x[0],
                        {
                            value: x[1],
                        },
                    ]))),
                })),
            }

        case "MAIN_LOADALL_START":
            return {
                ...state,
                loading: true,  
            }

        default:
            return state
    }
}