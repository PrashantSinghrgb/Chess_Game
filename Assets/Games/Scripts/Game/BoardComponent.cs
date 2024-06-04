using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    public class BoardComponent : MonoBehaviour
    {
        // the x- value
        public int x
        {
            get;
            private set;
        }

        // the y- value
        public int y
        {
            get;
            private set;
        }

        // Sets the x and y value
        ///<param name="x">The X Value</param>        
        ///<param name="y">The Y Value</param>
        ///<param name="automaticallyUpdateTransform">Whether the transform position should update. Defaults to true</param>
        
        public virtual void SetXY(int x, int y, bool automaticallyUpdateTransform = true)
        {
            Assert.IsTrue(x >= 0 && x <= GameBoard.ROWS, string.Format("{0} is an invalid x-value", x));
            Assert.IsTrue(y >= 0 && y <= GameBoard.COLUMS, string.Format("{0} is an invalid y-value", y));

            this.x = x; this.y = y;
            if (automaticallyUpdateTransform) { transform.localPosition = new Vector3(x, y, 0); }
        }
    }
}