package com.example.helloworld;

import org.junit.Before;
import org.junit.Test;

import static junit.framework.Assert.assertEquals;
import static org.junit.Assert.*;

/**
 * Example local unit test, which will execute on the development machine (host).
 *
 * @see <a href="http://d.android.com/tools/testing">Testing documentation</a>
 */
public class ExampleUnitTest {
    private CalculateMinMax calcMinMax;

    @Before
    public void setUp() throws Exception {
        calcMinMax = new CalculateMinMax();
    }

    @Test
    public void CalculateMinMax(){
        assertEquals(1, calcMinMax.Min(1,2));
        assertEquals(9, calcMinMax.Min(10,9));
        assertEquals(-2, calcMinMax.Min(-2,-1));
        assertEquals(0, calcMinMax.Min(0,0));

        assertEquals(2, calcMinMax.Max(1,2));
        assertEquals(10, calcMinMax.Max(10,9));
        assertEquals(-1, calcMinMax.Max(-2,-1));
        assertEquals(0, calcMinMax.Max(0,0));
    }
}