import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "NEWS_SELECT":
            return {
                ...state,
                editNews:
                {
                    ...state.editNews,
                    [action.name]:
                    {
                        value: action.val,
                    },
                }
            }

        case "NEWS_LOAD_PARAMETERS_SUCCESS":
            return {
                ...state,
                editNews:
                {
                    ...state.editNews,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {
                            value: state.editNews[x[0]].value,
                            options: x[1],
                        }
                    ])))
                }

            }

        case "NEWS_LOAD_SUCCESS":
            return {
                ...state,
                editNews:
                {
                    ...state.editNews,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {
                            value: x[1],
                        }
                    ]))),
                }
            }

        default:
            return state
    }
}